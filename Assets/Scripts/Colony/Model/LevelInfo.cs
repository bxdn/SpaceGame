using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Registry;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.Registry.GoodsServicesRegistry;

namespace Assets.Scripts.Model
{
    [Serializable]
    public class LevelInfo : ILevelInfo
    {
        [field: NonSerialized] public IDictionary<GoodOrService, float> GoodsServicesPerPopNeeds { get; private set; }
        private Dictionary<GoodOrService, float> goodsServicesPerPopNeeds = new Dictionary<GoodOrService, float>();
        [field: NonSerialized] public IDictionary<GoodOrService, float> GoodsServicesPerPopWants { get; private set; }
        private Dictionary<GoodOrService, float> goodsServicesPerPopWants = new Dictionary<GoodOrService, float>();
        private static readonly ImmutableDictionary<GoodOrService, float> tier1Wants = CreateTier1Wants();
        public int ServiceDistance { get; private set; } = 3;
        public int CurrentLevel { get; private set; }
        private readonly WantsNeedsAdjuster adjuster;
        public LevelInfo()
        {
            adjuster = new WantsNeedsAdjuster(this);
            CurrentLevel = 0;
            goodsServicesPerPopNeeds.Add(RegistryUtil.GoodsServices.Get(RegistryUtil.FOOD), .1f);
            goodsServicesPerPopNeeds.Add(RegistryUtil.GoodsServices.Get(RegistryUtil.WATER), .1f);
            goodsServicesPerPopNeeds.Add(RegistryUtil.GoodsServices.Get(RegistryUtil.ENERGY), .1f);
            goodsServicesPerPopWants.Add(RegistryUtil.GoodsServices.Get(RegistryUtil.ALCOHOL), .1f);
            goodsServicesPerPopNeeds.Add(RegistryUtil.GoodsServices.Get(RegistryUtil.HOUSING), 1);
            CreateImmutables();
        }
        public void LevelUp()
        {
            CurrentLevel++;
            ServiceDistance++;
            adjuster.ProcessGoodsAndServices();
            AddWant();
            CreateImmutables();
            WorldMapRenderController.ShowBuildableSquares();
        }
        private void AddWant()
        {
            var keys = tier1Wants.Keys.ToArray();
            var newWant = keys[ColonizerR.r.Next(keys.Length)];
            AddTier1WantGoodOrService(newWant);
        }
        private void AddTier1WantGoodOrService(GoodOrService g)
        {
            if (goodsServicesPerPopWants.ContainsKey(g))
                goodsServicesPerPopWants[g] += tier1Wants[g];
            else
                goodsServicesPerPopWants[g] = tier1Wants[g];
        }
        private void CreateImmutables()
        {
            GoodsServicesPerPopNeeds = goodsServicesPerPopNeeds.ToImmutableDictionary();
            GoodsServicesPerPopWants = goodsServicesPerPopWants.ToImmutableDictionary();
        }
        private static ImmutableDictionary<GoodOrService, float> CreateTier1Wants()
        {
            var wantsBuilder = ImmutableDictionary.CreateBuilder<GoodOrService, float>();
            wantsBuilder.Add(RegistryUtil.GoodsServices.Get("Windows"), .025f);
            wantsBuilder.Add(RegistryUtil.GoodsServices.Get(RegistryUtil.WATER), .05f);
            wantsBuilder.Add(RegistryUtil.GoodsServices.Get(RegistryUtil.FOOD), .05f);
            wantsBuilder.Add(RegistryUtil.GoodsServices.Get(RegistryUtil.ALCOHOL), .05f);
            wantsBuilder.Add(RegistryUtil.GoodsServices.Get("Education"), .1f);
            wantsBuilder.Add(RegistryUtil.GoodsServices.Get("Healthcare"), .1f);
            return wantsBuilder.ToImmutable();
        }
        public void FinishDeserialization() 
        { 
            CreateImmutables(); 
        }
        [Serializable]
        private class WantsNeedsAdjuster
        {
            private readonly IList<GoodOrService> toRemove = new List<GoodOrService>();
            private readonly LevelInfo outer;
            public WantsNeedsAdjuster(LevelInfo outer)
            {
                this.outer = outer;
            }
            public void ProcessGoodsAndServices()
            {
                toRemove.Clear();
                foreach (var pair in outer.goodsServicesPerPopWants)
                    PerhapsAddGoodOrServiceToNeeds(pair.Key, pair.Value);
                foreach (var good in toRemove)
                    outer.goodsServicesPerPopWants.Remove(good);
            }
            private void PerhapsAddGoodOrServiceToNeeds(GoodOrService key, float value)
            {
                if (ColonizerR.r.Next(100) > 50)
                    AddGoodOrServiceToNeeds(key, value);
            }
            private void AddGoodOrServiceToNeeds(GoodOrService key, float value)
            {
                if (outer.goodsServicesPerPopNeeds.ContainsKey(key))
                    outer.goodsServicesPerPopNeeds[key] += value;
                else
                    outer.goodsServicesPerPopNeeds[key] = value;
                toRemove.Add(key);
            }
        }
    }
}
