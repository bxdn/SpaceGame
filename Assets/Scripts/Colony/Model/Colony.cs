using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Registry;
using Assets.Scripts.Trade.Model;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Registry.GoodsServicesRegistry;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class Colony
    {
        private readonly IDictionary<GoodOrService, GoodInfo> goods = new Dictionary<GoodOrService, GoodInfo>();
        private IDictionary<GoodOrService, GoodInfo> prevGoods;
        private readonly IDictionary<GoodOrService, float> services = new Dictionary<GoodOrService, float>();
        private readonly IDictionary<StructureInfo, IList<Structure>> structures = new Dictionary<StructureInfo, IList<Structure>>();
        public float Influence { get; private set; } = 100;
        public int Population { get; private set; } = 0;
        public IDictionary<GoodOrService, GoodInfo> Goods
        {
            get => new Dictionary<GoodOrService, GoodInfo>(goods);
        }
        public IDictionary<GoodOrService, float> Services
        {
            get => new Dictionary<GoodOrService, float>(services);
        }
        public IDictionary<StructureInfo, IList<Structure>> Structures
        {
            get => new Dictionary<StructureInfo, IList<Structure>>(structures);
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
        public void IncrementGood(GoodOrService good, float amount)
        {
            if (goods.ContainsKey(good))
                goods[good] = new GoodInfo(goods[good].Value + amount);
            else
                goods[good] = new GoodInfo(amount);
        }
        private void IncrementNeededGood(GoodOrService good, float amount)
        {
            IncrementGood(good, amount);
            float newValue = goods[good].Value;
            if (newValue < 0)
            {
                goods[good] = new GoodInfo(0);
                IncrementInfluence(newValue);
            }
        }
        public bool CanBuildStructure(StructureInfo structure)
        {
            var hq = RegistryUtil.Structures.GetStructure("HQ");
            if (structure == hq)
                return true;
            var housing = RegistryUtil.Structures.GetStructure("Housing");
            if (structure == housing && !CanBeSettled())
                return false;
            if (structure.WorkerLevel > Level.CurrentLevel)
                return false;
            var toRet = true;
            var enumerator = structure.GoodCost.GetEnumerator();
            enumerator.Reset();
            KeyValuePair<string, float> current;
            while (toRet && enumerator.MoveNext())
            {
                current = enumerator.Current;
                var good = RegistryUtil.GoodsServices.Get(current.Key);
                toRet &= goods.ContainsKey(good) && goods[good].Value >= current.Value;
            }
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
        private void IncrementWantedGood(GoodOrService good, float amount)
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
        private void IncrementService(GoodOrService service, float amount)
        {
            services[service] = services.ContainsKey(service) ? services[service] + amount : amount;
        }
        private void CreateStructure(StructureInfo structure, Structure struc)
        {
            if (!structures.ContainsKey(structure))
                structures[structure] = new List<Structure>();
            structures[structure].Add(struc);
        }
        private void ProcessServiceDemand(KeyValuePair<GoodOrService, float> servicePerPop)
        {
            float demand = servicePerPop.Value * Population;
            if (!services.ContainsKey(servicePerPop.Key))
                IncrementInfluence(-demand);
            else if (services[servicePerPop.Key] < demand)
                IncrementInfluence(services[servicePerPop.Key] - demand);
        }
        private void ProcessServiceWant(KeyValuePair<GoodOrService, float> servicePerPop)
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
            foreach (var demandInfo in level.GoodsServicesPerPopNeeds)
                ProcessNeededGoodOrService(demandInfo);
            foreach (var wantInfo in level.GoodsServicesPerPopWants)
                ProcessWantedGoodOrService(wantInfo);
            var housing = RegistryUtil.GoodsServices.Get("Housing");
            if (services.ContainsKey(housing)
                && services[housing] > Population * level.GoodsServicesPerPopNeeds[housing])
                IncrementPop(level);
            if (Influence >= Mathf.Pow(2, level.CurrentLevel) * 200 && manager.Habitability >= 90)
                level.LevelUp();
        }
        private void ProcessNeededGoodOrService(KeyValuePair<GoodOrService,float> demandInfo)
        {
            if (RegistryUtil.GoodsServices.IsService(demandInfo.Key))
                ProcessServiceDemand(demandInfo);
            else
                IncrementNeededGood(demandInfo.Key, -demandInfo.Value * Population);
        }
        private void ProcessWantedGoodOrService(KeyValuePair<GoodOrService, float> demandInfo)
        {
            if (RegistryUtil.GoodsServices.IsService(demandInfo.Key))
                ProcessServiceWant(demandInfo);
            else
                IncrementWantedGood(demandInfo.Key, -demandInfo.Value * Population);
        }
        public void FinishGoodsCalculations()
        {
            foreach (var good in goods)
                AssignDirection(prevGoods, good);
        }
        private void AssignDirection(IDictionary<GoodOrService, GoodInfo> prevGoods, KeyValuePair<GoodOrService, GoodInfo> good)
        {
            if (!prevGoods.ContainsKey(good.Key) || good.Value.Value > prevGoods[good.Key].Value)
                good.Value.Increasing = 1;
            else if (good.Value.Value == prevGoods[good.Key].Value)
                good.Value.Increasing = 0;
            else
                good.Value.Increasing = -1;
        }
        private void WorkStructures(KeyValuePair<StructureInfo, IList<Structure>> structurePair)
        {
            foreach(Structure structure in structurePair.Value)
                WorkStructure(structurePair.Key, structure.Multiplier);
        }
        private void IncrementPop(LevelInfo level)
        {
            var growthCap = Math.Max(1, .05 * Population);
            var housing = RegistryUtil.GoodsServices.Get("Housing");
            var housingCap = services[housing] - Population * level.GoodsServicesPerPopNeeds[housing];
            int amountToIncrease = (int)Math.Min(growthCap, housingCap);
            Population += amountToIncrease;
        }
        private void WorkStructure(StructureInfo structure, float multiplier)
        {
            bool validGoods = true;
            foreach (var numGoodsPerStruc in structure.Flow)
            {
                var good = RegistryUtil.GoodsServices.Get(numGoodsPerStruc.Key);
                validGoods &= numGoodsPerStruc.Value > 0 || goods.ContainsKey(good) && goods[good].Value * multiplier >= -numGoodsPerStruc.Value;
            }
            if (!validGoods)
                return;
            foreach (var goodInfo in structure.Flow)
                IncrementGood(RegistryUtil.GoodsServices.Get(goodInfo.Key), goodInfo.Value * multiplier);
        }
        public void AddStructure(StructureInfo structure, int idx)
        {
            var strucCoords = new SVector2Int(Utils.IdxToSquareCoords(idx, Utils.GetRowSize(manager.Size)));
            var goodsMultiplier = 1 / Mathf.Pow(Utils.GetDistance(idx, Location, Utils.GetRowSize(manager.Size)), .5f);
            var struc = new Structure(strucCoords, goodsMultiplier);
            CreateStructure(structure, struc);
            var info = structure;
            foreach (var pair in info.GoodCost)
                IncrementGood(RegistryUtil.GoodsServices.Get(pair.Key), -pair.Value);
            foreach (var pair in info.ServiceFlow)
                IncrementService(RegistryUtil.GoodsServices.Get(pair.Key), pair.Value);
        }
        public bool CanIncrementGood(GoodOrService good, float value)
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
