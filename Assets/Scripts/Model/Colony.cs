using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class Colony
    {
        private readonly IDictionary<EGood, GoodInfo> goods = new Dictionary<EGood, GoodInfo>();
        private readonly IDictionary<EService, float> services = new Dictionary<EService, float>();
        private readonly IDictionary<EResource, int> resources = new Dictionary<EResource, int>();
        private readonly IDictionary<StructureInfo, int> structures = new Dictionary<StructureInfo, int>();
        public float Influence { get; private set; } = 100;
        public int Workers { get; set; } = 100;
        public int Population { get; private set; } = 150;
        public IDictionary<EGood, GoodInfo> Goods
        {
            get => new Dictionary<EGood, GoodInfo>(goods);
        }
        public IDictionary<EService, float> Services
        {
            get => new Dictionary<EService, float>(services);
        }
        public IDictionary<EResource, int> Resources
        {
            get => new Dictionary<EResource, int>(resources);
        }
        public IDictionary<StructureInfo, int> Structures
        {
            get => new Dictionary<StructureInfo, int>(structures);
        }
        public LevelInfo CurrentLevel { get; private set; } = LevelInfo.FirstLevel;
        public Colony(IDictionary<EResource, int> resources)
        {
            ColonyUpdater.AddColony(this);
            this.resources = resources;
            goods[EGood.Food] = new GoodInfo(100);
            goods[EGood.Water] = new GoodInfo(100);
            goods[EGood.BuildingMaterials] = new GoodInfo(100);
            goods[EGood.Energy] = new GoodInfo(100);
            for (int i = 0; i < 3; i ++)
                AddStructure(StructureRegistry.Housing);
        }
        private void IncrementGood(EGood good, float amount)
        {
            if (goods.ContainsKey(good))
                goods[good] = new GoodInfo(goods[good].Value + amount);
            else
                goods[good] = new GoodInfo(amount);
            if (goods[good].Value < 0)
                TakeGoodPenalty(good);
        }
        private void TakeGoodPenalty(EGood good)
        {
            IncrementInfluence(goods[good].Value);
            goods[good] = new GoodInfo(0);
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
        private void IncrementStructure(StructureInfo info, int amount)
        {
            structures[info] = structures.ContainsKey(info) ? structures[info] + amount : amount;
        }
        private void ProcessServiceDemand(KeyValuePair<EService, float> servicePerPop)
        {
            float demand = servicePerPop.Value * Population;
            if (!services.ContainsKey(servicePerPop.Key))
                IncrementInfluence(-demand);
            else if (services[servicePerPop.Key] < demand)
                IncrementInfluence(services[servicePerPop.Key] - demand);
        }
        public void TickForward()
        {
            Debug.Log(Influence);
            var prevGoods = Goods;
            foreach (var structurePair in structures)
                WorkStructures(structurePair);
            foreach (var goodPair in CurrentLevel.GoodsPerPop)
                IncrementGood(goodPair.Key, (-goodPair.Value / 100f) * Population);
            foreach (var good in this.goods)
                good.Value.Increasing = !prevGoods.ContainsKey(good.Key) || good.Value.Value >= prevGoods[good.Key].Value;
            foreach (var service in CurrentLevel.ServicesPerPop)
                ProcessServiceDemand(service);
            if (services.ContainsKey(EService.Housing) 
                && services[EService.Housing] > Population * CurrentLevel.ServicesPerPop[EService.Housing])
                IncrementPop();
            Influence++;
            if (Influence >= 200)
                LevelUp();
        }
        private void LevelUp()
        {
            if(CurrentLevel.Next != null)
                CurrentLevel = CurrentLevel.Next;
            Influence = 100;
        }
        private void WorkStructures(KeyValuePair<StructureInfo, int> structurePair)
        {
            for (int i = 0; i < structurePair.Value; i++)
                WorkStructure(structurePair.Key);
        }
        private void IncrementPop()
        {
            int amountToIncrease = (int) Math.Min(.01 * Population, services[EService.Housing] - Population * CurrentLevel.ServicesPerPop[EService.Housing]);
            Population += amountToIncrease;
            for (int i = 0; i < amountToIncrease; i++)
                IncrementWorkers();
        }
        private void IncrementWorkers()
        {
            if (ColonizerR.r.Next(100) > 33)
                Workers++;
        }
        private void WorkStructure(StructureInfo structure)
        {
            foreach (var goodInfo in structure.Flow)
                IncrementGood(goodInfo.Key, goodInfo.Value);
        }
        public void AddStructure(StructureInfo info)
        {
            IncrementStructure(info, 1);
            foreach (var pair in info.GoodCost)
                IncrementGood(pair.Key, -pair.Value);
            foreach (var pair in info.ServiceFlow)
                IncrementService(pair.Key, pair.Value);
            Workers -= info.RequiredWorkers;
        }
    }
}
