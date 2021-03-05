using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class Colony
    {
        private static readonly int MAX_HOUSING_RADIUS = 5;
        private readonly IDictionary<EGood, GoodInfo> goods = new Dictionary<EGood, GoodInfo>();
        private readonly IDictionary<EService, float> services = new Dictionary<EService, float>();
        private readonly IDictionary<EStructure, IList<Structure>> structures = new Dictionary<EStructure, IList<Structure>>();
        public float Influence { get; private set; } = 100;
        public int Population { get; private set; } = 0;
        public int Rockets { get; private set; } = 0;
        public IDictionary<EGood, GoodInfo> Goods
        {
            get => new Dictionary<EGood, GoodInfo>(goods);
        }
        public IDictionary<EService, float> Services
        {
            get => new Dictionary<EService, float>(services);
        }
        public IDictionary<EStructure, IList<Structure>> Structures
        {
            get => new Dictionary<EStructure, IList<Structure>>(structures);
        }
        private readonly IList<int> logisticStructures = new List<int>();
        public LevelInfo LevelInfo { get; } = new LevelInfo();
        private readonly IColonizableManager manager;
        public Colony(IColonizableManager manager)
        {
            ColonyUpdater.AddColony(this);
            this.manager = manager;
            goods[EGood.Energy] = new GoodInfo(100);
            goods[EGood.Chips] = new GoodInfo(100);
            goods[EGood.Steel] = new GoodInfo(100);
        }
        public void AddLogisticStructure(int idx)
        {
            logisticStructures.Add(idx);
        }
        private void IncrementGood(EGood good, float amount)
        {
            if (goods.ContainsKey(good))
                goods[good] = new GoodInfo(goods[good].Value + amount);
            else
                goods[good] = new GoodInfo(amount);
        }
        private void IncrementNeededGood(EGood good, float amount)
        {
            IncrementGood(good, amount);
            float newValue = goods[good].Value;
            if (newValue < 0)
            {
                goods[good] = new GoodInfo(0);
                IncrementInfluence(newValue);
            }
        }

        public bool IsServicePlaceable(int serviceIdx)
        {
            var rowSize = Utils.GetRowSize(manager.Size);
            var isHousingPlaceable = false;
            for (int i = 0; i < logisticStructures.Count && isHousingPlaceable == false; i++)
                isHousingPlaceable = IsServiceNearLogisticsStructure(serviceIdx, logisticStructures[i], rowSize);
            return isHousingPlaceable;
        }
        private bool IsServiceNearLogisticsStructure(int housingIdx, int logisticsIdx, int rowSize)
        {
            var potentialHousingCoords = Utils.IdxToSquareCoords(housingIdx, rowSize);
            var logisticStructureCoords = Utils.IdxToSquareCoords(logisticsIdx, rowSize);
            var distance = Utils.GetDistance(potentialHousingCoords, logisticStructureCoords);
            return  distance < MAX_HOUSING_RADIUS;
        }

        private void IncrementWantedGood(EGood good, float amount)
        {
            IncrementGood(good, amount);
            float newValue = goods[good].Value;
            if (newValue < 0)
            {
                goods[good] = new GoodInfo(0);
                var wantThreshold = Mathf.Pow(2, LevelInfo.CurrentLevel) * 100;
                if (Influence > wantThreshold)
                    IncrementInfluence(Math.Max(wantThreshold - Influence, newValue));
            }
        }
        private void IncrementInfluence(float value)
        {
            Influence += value;
            if (Influence < 0)
                Debug.Log("YOU LOSE!!!");
        }
        private void IncrementService(EService service, float amount)
        {
            services[service] = services.ContainsKey(service) ? services[service] + amount : amount;
        }
        private void CreateStructure(EStructure structure, Structure struc)
        {
            if (!structures.ContainsKey(structure))
                structures[structure] = new List<Structure>();
            structures[structure].Add(struc);
        }
        private void ProcessServiceDemand(KeyValuePair<EService, float> servicePerPop)
        {
            float demand = servicePerPop.Value * Population;
            if (!services.ContainsKey(servicePerPop.Key))
                IncrementInfluence(-demand);
            else if (services[servicePerPop.Key] < demand)
                IncrementInfluence(services[servicePerPop.Key] - demand);
        }
        private void ProcessServiceWant(KeyValuePair<EService, float> servicePerPop)
        {
            var wantThreshold = Mathf.Pow(2, LevelInfo.CurrentLevel) * 100;
            if (Influence <= wantThreshold)
                return;
            float demand = servicePerPop.Value * Population;
            float maxInfluenceHit = wantThreshold - Influence;
            if (!services.ContainsKey(servicePerPop.Key))
                IncrementInfluence(Math.Max(maxInfluenceHit, -demand));
            else if (services[servicePerPop.Key] < demand)
                IncrementInfluence(Math.Max(maxInfluenceHit, services[servicePerPop.Key] - demand));
        }
        public void TickForward()
        {
            if(Population > 0)
                Influence += Mathf.Pow(Population/100f, .5f);
            var prevGoods = Goods;
            foreach (var structurePair in structures)
                WorkStructures(structurePair);
            foreach (var goodPair in LevelInfo.GoodsPerPopNeeds)
                IncrementNeededGood(goodPair.Key, (-goodPair.Value) * Population);
            foreach (var goodPair in LevelInfo.GoodsPerPopWants)
                IncrementWantedGood(goodPair.Key, (-goodPair.Value) * Population);
            foreach (var good in goods)
                AssignDirection(prevGoods, good);
            foreach (var service in LevelInfo.ServicesPerPopNeeds)
                ProcessServiceDemand(service);
            foreach (var service in LevelInfo.ServicesPerPopWants)
                ProcessServiceWant(service);
            if (services.ContainsKey(EService.Housing)
                && services[EService.Housing] > Population * LevelInfo.ServicesPerPopNeeds[EService.Housing])
                IncrementPop(LevelInfo);
            if (Influence >= Mathf.Pow(2, LevelInfo.CurrentLevel) * 200 && manager.Habitability >= 90)
                LevelInfo.LevelUp();
        }
        private void AssignDirection(IDictionary<EGood, GoodInfo> prevGoods, KeyValuePair<EGood, GoodInfo> good)
        {
            if (!prevGoods.ContainsKey(good.Key) || good.Value.Value > prevGoods[good.Key].Value)
                good.Value.Increasing = 1;
            else if (good.Value.Value == prevGoods[good.Key].Value)
                good.Value.Increasing = 0;
            else
                good.Value.Increasing = -1;
        }
        private void WorkStructures(KeyValuePair<EStructure, IList<Structure>> structurePair)
        {
            foreach(Structure structure in structurePair.Value)
                WorkStructure(structurePair.Key, structure.Multiplier);
        }
        private void IncrementPop(LevelInfo level)
        {
            var maxIncrease = Math.Max(1, .01 * Population);
            var potentialIncrease = services[EService.Housing] - Population * level.ServicesPerPopNeeds[EService.Housing];
            int amountToIncrease = (int)Math.Min(maxIncrease, potentialIncrease);
            Population += amountToIncrease;
        }
        private void WorkStructure(EStructure structure, float multiplier)
        {
            bool validGoods = true;
            foreach (var goodInfo in ((StructureInfo)Constants.FEATURE_MAP[structure]).Flow)
                validGoods &= goodInfo.Value > 0 || goods.ContainsKey(goodInfo.Key) && goods[goodInfo.Key].Value * multiplier >= -goodInfo.Value;
            if (!validGoods)
                return;
            foreach (var goodInfo in ((StructureInfo)Constants.FEATURE_MAP[structure]).Flow)
                IncrementGood(goodInfo.Key, goodInfo.Value * multiplier);
        }
        public void AddStructure(EStructure structure, int idx)
        {
            var strucCoords = new SVector2Int(Utils.IdxToSquareCoords(idx, Utils.GetRowSize(manager.Size)));
            var goodsMultiplier = 1 / Mathf.Pow(DistanceToNearestHub(idx), .5f);
            var struc = new Structure(strucCoords, goodsMultiplier);
            CreateStructure(structure, struc);
            var info = (StructureInfo) Constants.FEATURE_MAP[structure];
            foreach (var pair in info.GoodCost)
                IncrementGood(pair.Key, -pair.Value);
            foreach (var pair in info.ServiceFlow)
                IncrementService(pair.Key, pair.Value);
        }
        private float DistanceToNearestHub(int idx)
        {
            var rowSize = Utils.GetRowSize(manager.Size);
            var strucCoords = Utils.IdxToSquareCoords(idx, rowSize);
            var minDistance = float.MaxValue;
            foreach (int hubIdx in logisticStructures)
                minDistance = MinDistance(strucCoords, hubIdx, rowSize, minDistance);
            return minDistance;
        }
        private float MinDistance(Vector2Int coords, int idx, int rowSize, float currentMin)
        {
            var coords2 = Utils.IdxToSquareCoords(idx, rowSize);
            var distance = Utils.GetDistance(coords, coords2);
            return Mathf.Min(distance, currentMin);
        }
        public void AddRocket()
        {
            IncrementGood(EGood.Hydrogen, -100);
            IncrementGood(EGood.Energy, -50);
            IncrementGood(EGood.Oxygen, -50);
            IncrementGood(EGood.Tools, -50);
            Rockets++;
        }
    }
}
