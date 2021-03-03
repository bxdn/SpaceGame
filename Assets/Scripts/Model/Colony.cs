using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class Colony
    {
        private readonly IDictionary<EGood, GoodInfo> goods = new Dictionary<EGood, GoodInfo>();
        private readonly IDictionary<EService, float> services = new Dictionary<EService, float>();
        private readonly IDictionary<EStructure, int> structures = new Dictionary<EStructure, int>();
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
        public IDictionary<EStructure, int> Structures
        {
            get => new Dictionary<EStructure, int>(structures);
        }
        public int CurrentLevel { get; private set; } = 0;
        public Colony(Galaxy g)
        {
            ColonyUpdater.AddColony(this, g);
            goods[EGood.Energy] = new GoodInfo(100);
            goods[EGood.Chips] = new GoodInfo(100);
            goods[EGood.Steel] = new GoodInfo(100);
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
        private void IncrementWantedGood(EGood good, float amount)
        {
            IncrementGood(good, amount);
            float newValue = goods[good].Value;
            if (newValue < 0)
            {
                goods[good] = new GoodInfo(0);
                if (Influence > 100)
                    IncrementInfluence(Math.Max(100 - Influence, newValue));
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
        private void IncrementStructure(EStructure structure, int amount)
        {
            structures[structure] = structures.ContainsKey(structure) ? structures[structure] + amount : amount;
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
            if (Influence <= 100)
                return;
            float demand = servicePerPop.Value * Population;
            float maxInfluenceHit = 100 - Influence;
            if (!services.ContainsKey(servicePerPop.Key))
                IncrementInfluence(Math.Max(maxInfluenceHit, -demand));
            else if (services[servicePerPop.Key] < demand)
                IncrementInfluence(Math.Max(maxInfluenceHit, services[servicePerPop.Key] - demand));
        }
        public void TickForward()
        {
            if(Population > 0)
                Influence++;
            var prevGoods = Goods;
            LevelInfo level = LevelInfo.GetLevel(Math.Min(CurrentLevel, 5));
            foreach (var structurePair in structures)
                WorkStructures(structurePair);
            foreach (var goodPair in level.GoodsPerPopNeeds)
                IncrementNeededGood(goodPair.Key, (-goodPair.Value / 100f) * Population);
            foreach (var goodPair in level.GoodsPerPopWants)
                IncrementWantedGood(goodPair.Key, (-goodPair.Value / 100f) * Population);
            foreach (var good in goods)
                AssignDirection(prevGoods, good);
            foreach (var service in level.ServicesPerPopNeeds)
                ProcessServiceDemand(service);
            foreach (var service in level.ServicesPerPopWants)
                ProcessServiceWant(service);
            if (services.ContainsKey(EService.Housing)
                && services[EService.Housing] > Population * level.ServicesPerPopNeeds[EService.Housing])
                IncrementPop(level);
            if (Influence >= 200)
                LevelUp();
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
        private void LevelUp()
        {
            CurrentLevel++;
            Influence = 100;
        }
        private void WorkStructures(KeyValuePair<EStructure, int> structurePair)
        {
            for (int i = 0; i < structurePair.Value; i++)
                WorkStructure(structurePair.Key);
        }
        private void IncrementPop(LevelInfo level)
        {
            var maxIncrease = Math.Max(1, .01 * Population);
            var potentialIncrease = services[EService.Housing] - Population * level.ServicesPerPopNeeds[EService.Housing];
            int amountToIncrease = (int)Math.Min(maxIncrease, potentialIncrease);
            Population += amountToIncrease;
        }
        private void WorkStructure(EStructure structure)
        {
            bool validGoods = true;
            foreach (var goodInfo in Constants.STRUCTURE_MAP[structure].Flow)
                validGoods &= goodInfo.Value > 0 || goods.ContainsKey(goodInfo.Key) && goods[goodInfo.Key].Value >= -goodInfo.Value;
            if (!validGoods)
                return;
            foreach (var goodInfo in Constants.STRUCTURE_MAP[structure].Flow)
                IncrementGood(goodInfo.Key, goodInfo.Value);
        }
        public void AddStructure(EStructure structure)
        {
            IncrementStructure(structure, 1);
            StructureInfo info = Constants.STRUCTURE_MAP[structure];
            foreach (var pair in info.GoodCost)
                IncrementGood(pair.Key, -pair.Value);
            foreach (var pair in info.ServiceFlow)
                IncrementService(pair.Key, pair.Value);
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
