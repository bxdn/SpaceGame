using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [Serializable]
    public class LevelInfo : ILevelInfo
    {
        [field: NonSerialized] public IDictionary<EGood, float> GoodsPerPopNeeds { get; private set; }
        private Dictionary<EGood, float> goodsPerPopNeeds = new Dictionary<EGood, float>();
        [field: NonSerialized] public IDictionary<EService, float> ServicesPerPopNeeds { get; private set; }
        private Dictionary<EService, float> servicesPerPopNeeds = new Dictionary<EService, float>();
        [field: NonSerialized] public IDictionary<EGood, float> GoodsPerPopWants { get; private set; }
        private Dictionary<EGood, float> goodsPerPopWants = new Dictionary<EGood, float>();
        [field: NonSerialized] public IDictionary<EService, float> ServicesPerPopWants { get; private set; }
        private Dictionary<EService, float> servicesPerPopWants = new Dictionary<EService, float>();
        private static ImmutableDictionary<Enum, float> tier1Wants = CreateTier1Wants();
        public int ServiceDistance { get; private set; } = 3;
        public int CurrentLevel { get; private set; }
        private readonly WantsNeedsAdjuster adjuster;
        public LevelInfo()
        {
            adjuster = new WantsNeedsAdjuster(this);
            CurrentLevel = 0;
            goodsPerPopNeeds.Add(EGood.Food, .1f);
            goodsPerPopNeeds.Add(EGood.Water, .1f);
            goodsPerPopNeeds.Add(EGood.Energy, .1f);
            goodsPerPopWants.Add(EGood.Alcohol, .1f);
            servicesPerPopNeeds.Add(EService.Housing, 1);
            CreateImmutables();
        }
        public void LevelUp()
        {
            CurrentLevel++;
            ServiceDistance++;
            adjuster.ProcessGoods();
            adjuster.ProcessServices();
            AddWant();
            CreateImmutables();
        }
        private void AddWant()
        {
            var keys = tier1Wants.Keys.ToArray();
            var newWant = keys[ColonizerR.r.Next(keys.Length)];
            if (newWant is EGood g)
                AddTier1WantGood(g);
            else if (newWant is EService s)
                AddTier1WantService(s);
        }
        private void AddTier1WantGood(EGood g)
        {
            if (goodsPerPopWants.ContainsKey(g))
                goodsPerPopWants[g] += tier1Wants[g];
            else
                goodsPerPopWants[g] = tier1Wants[g];
        }
        private void CreateImmutables()
        {
            GoodsPerPopNeeds = goodsPerPopNeeds.ToImmutableDictionary();
            ServicesPerPopNeeds = servicesPerPopNeeds.ToImmutableDictionary();
            GoodsPerPopWants = goodsPerPopWants.ToImmutableDictionary();
            ServicesPerPopWants = servicesPerPopWants.ToImmutableDictionary();
        }
        private void AddTier1WantService(EService s)
        {
            if (servicesPerPopWants.ContainsKey(s))
                servicesPerPopWants[s] += tier1Wants[s];
            else
                servicesPerPopWants[s] = tier1Wants[s];
        }
        private static ImmutableDictionary<Enum, float> CreateTier1Wants()
        {
            var wantsBuilder = ImmutableDictionary.CreateBuilder<Enum, float>();
            wantsBuilder.Add(EGood.Windows, .025f);
            wantsBuilder.Add(EGood.Water, .05f);
            wantsBuilder.Add(EGood.Food, .05f);
            wantsBuilder.Add(EGood.Alcohol, .05f);
            wantsBuilder.Add(EService.Education, .1f);
            wantsBuilder.Add(EService.Healthcare, .1f);
            return wantsBuilder.ToImmutable();
        }
        public void FinishDeserialization() 
        { 
            CreateImmutables(); 
        }
        [Serializable]
        private class WantsNeedsAdjuster
        {
            private readonly IList<Enum> toRemove = new List<Enum>();
            private readonly LevelInfo outer;
            public WantsNeedsAdjuster(LevelInfo outer)
            {
                this.outer = outer;
            }
            public void ProcessGoods()
            {
                toRemove.Clear();
                foreach (var pair in outer.goodsPerPopWants)
                    PerhapsAddGoodToNeeds(pair.Key, pair.Value);
                foreach (var good in toRemove)
                    outer.goodsPerPopWants.Remove((EGood)good);
            }
            private void PerhapsAddGoodToNeeds(EGood key, float value)
            {
                if (ColonizerR.r.Next(100) > 50)
                    AddGoodToNeeds(key, value);
            }
            private void AddGoodToNeeds(EGood key, float value)
            {
                if (outer.goodsPerPopNeeds.ContainsKey(key))
                    outer.goodsPerPopNeeds[key] += value;
                else
                    outer.goodsPerPopNeeds[key] = value;
                toRemove.Add(key);
            }
            public void ProcessServices()
            {
                toRemove.Clear();
                foreach (var pair in outer.servicesPerPopWants)
                    PerhapsAddServiceToNeeds(pair.Key, pair.Value);
                foreach (var service in toRemove)
                    outer.servicesPerPopWants.Remove((EService)service);
            }
            private void PerhapsAddServiceToNeeds(EService key, float value)
            {
                if (ColonizerR.r.Next(100) > 50)
                    AddServiceToNeeds(key, value);
            }
            private void AddServiceToNeeds(EService key, float value)
            {
                if (outer.servicesPerPopNeeds.ContainsKey(key))
                    outer.servicesPerPopNeeds[key] += value;
                else
                    outer.servicesPerPopNeeds[key] = value;
                toRemove.Add(key);
            }
        }
    }
}
