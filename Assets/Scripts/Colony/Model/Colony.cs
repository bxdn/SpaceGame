using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Trade.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class Colony
    {
        private readonly IDictionary<EGood, GoodInfo> goods = new Dictionary<EGood, GoodInfo>();
        private IDictionary<EGood, GoodInfo> prevGoods;
        private readonly IDictionary<EService, float> services = new Dictionary<EService, float>();
        private readonly IDictionary<EStructure, IList<Structure>> structures = new Dictionary<EStructure, IList<Structure>>();
        public float Influence { get; private set; } = 100;
        public int Population { get; private set; } = 0;
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
        public TradeManager TradeManager { get; } = new TradeManager();

        public ILevelInfo Level { get => level; }
        private readonly LevelInfo level = new LevelInfo();
    
        public int Location { get; }
        private readonly IColonizableManager manager;

        public string Name { get; set; } = "";
        public Colony(IColonizableManager manager, int idx)
        {
            ColonyUpdater.AddColony(this);
            this.manager = manager;
            Location = idx;
        }
        public void IncrementGood(EGood good, float amount)
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
        public bool CanBuildStructure(EStructure structure)
        {
            if (structure == EStructure.HQ)
                return true;
            if (structure == EStructure.Housing && !CanBeSettled())
                return false;
            var info = (StructureInfo)Constants.FEATURE_MAP[structure];
            if (info.WorkerLevel > Level.CurrentLevel)
                return false;
            var toRet = true;
            var enumerator = info.GoodCost.GetEnumerator();
            enumerator.Reset();
            KeyValuePair<EGood, int> current;
            while (toRet && enumerator.MoveNext())
                toRet &= goods.ContainsKey((current = enumerator.Current).Key) && goods[current.Key].Value >= current.Value;
            return toRet;
        }
        public bool IsIndustryPlaceable(int serviceIdx)
        {
            var rowSize = Utils.GetRowSize(manager.Size);
            return IsIndustryNearHQ(serviceIdx, Location, rowSize);
        }
        public bool IsServicePlaceable(int serviceIdx)
        {
            var rowSize = Utils.GetRowSize(manager.Size);
            return IsServiceNearHQ(serviceIdx, Location, rowSize);
        }
        private bool IsServiceNearHQ(int serviceIdx, int logisticsIdx, int rowSize)
        {
            var potentialHousingCoords = Utils.IdxToSquareCoords(serviceIdx, rowSize);
            var logisticStructureCoords = Utils.IdxToSquareCoords(logisticsIdx, rowSize);
            var distance = Utils.GetDistance(potentialHousingCoords, logisticStructureCoords);
            return  distance < level.ServiceDistance;
        }
        private bool IsIndustryNearHQ(int serviceIdx, int logisticsIdx, int rowSize)
        {
            var potentialIndustryCoords = Utils.IdxToSquareCoords(serviceIdx, rowSize);
            var logisticStructureCoords = Utils.IdxToSquareCoords(logisticsIdx, rowSize);
            var distance = Utils.GetDistance(potentialIndustryCoords, logisticStructureCoords);
            return distance < level.ServiceDistance * 2;
        }
        private void IncrementWantedGood(EGood good, float amount)
        {
            IncrementGood(good, amount);
            float newValue = goods[good].Value;
            if (newValue < 0)
            {
                goods[good] = new GoodInfo(0);
                var wantThreshold = Mathf.Pow(2, level.CurrentLevel) * 100;
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
            var wantThreshold = Mathf.Pow(2, level.CurrentLevel) * 100;
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
                Influence += Mathf.Pow(Population/1000f, .5f);
            prevGoods = Goods;
            TradeManager.ProcessTrades();
            foreach (var structurePair in structures)
                WorkStructures(structurePair);
            foreach (var goodPair in level.GoodsPerPopNeeds)
                IncrementNeededGood(goodPair.Key, (-goodPair.Value) * Population);
            foreach (var goodPair in level.GoodsPerPopWants)
                IncrementWantedGood(goodPair.Key, (-goodPair.Value) * Population);
            foreach (var service in level.ServicesPerPopNeeds)
                ProcessServiceDemand(service);
            foreach (var service in level.ServicesPerPopWants)
                ProcessServiceWant(service);
            if (services.ContainsKey(EService.Housing)
                && services[EService.Housing] > Population * level.ServicesPerPopNeeds[EService.Housing])
                IncrementPop(level);
            if (Influence >= Mathf.Pow(2, level.CurrentLevel) * 200 && manager.Habitability >= 90)
                level.LevelUp();
        }
        public void FinishGoodsCalculations()
        {
            foreach (var good in goods)
                AssignDirection(prevGoods, good);
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
            var growthCap = Math.Max(1, .05 * Population);
            var housingCap = services[EService.Housing] - Population * level.ServicesPerPopNeeds[EService.Housing];
            int amountToIncrease = (int)Math.Min(growthCap, housingCap);
            Population += amountToIncrease;
        }
        private void WorkStructure(EStructure structure, float multiplier)
        {
            bool validGoods = true;
            foreach (var numGoodsPerStruc in ((StructureInfo)Constants.FEATURE_MAP[structure]).Flow)
                validGoods &= numGoodsPerStruc.Value > 0 || goods.ContainsKey(numGoodsPerStruc.Key) && goods[numGoodsPerStruc.Key].Value * multiplier >= -numGoodsPerStruc.Value;
            if (!validGoods)
                return;
            foreach (var goodInfo in ((StructureInfo)Constants.FEATURE_MAP[structure]).Flow)
                IncrementGood(goodInfo.Key, goodInfo.Value * multiplier);
        }
        public void AddStructure(EStructure structure, int idx)
        {
            var strucCoords = new SVector2Int(Utils.IdxToSquareCoords(idx, Utils.GetRowSize(manager.Size)));
            var goodsMultiplier = 1 / Mathf.Pow(Utils.GetDistance(idx, Location, Utils.GetRowSize(manager.Size)), .5f);
            var struc = new Structure(strucCoords, goodsMultiplier);
            CreateStructure(structure, struc);
            var info = (StructureInfo) Constants.FEATURE_MAP[structure];
            foreach (var pair in info.GoodCost)
                IncrementGood(pair.Key, -pair.Value);
            foreach (var pair in info.ServiceFlow)
                IncrementService(pair.Key, pair.Value);
        }
        public bool CanIncrementGood(EGood good, float value)
        {
            if (value >= 0)
                return true;
            if (!goods.ContainsKey(good))
                return false;
            return goods[good].Value + value >= 0;
        }
        public void FinishDeserialization()
        {
            level.FinishDeserialization();
        }
        public bool CanBeSettled()
        {
            return manager.Habitability >= 90;
        }
    }
}
