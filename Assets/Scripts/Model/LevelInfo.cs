using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [Serializable]
    public class LevelInfo
    {
        public ImmutableDictionary<EGood, float> GoodsPerPopNeeds { get; }
        public ImmutableDictionary<EService, float> ServicesPerPopNeeds { get; }
        public ImmutableDictionary<EGood, float> GoodsPerPopWants { get; }
        public ImmutableDictionary<EService, float> ServicesPerPopWants { get; }
        public int Idx { get; }
        private static readonly int LEVELS = 6;
        private static readonly LevelInfo[] levels = CreateLevels();
        public LevelInfo(ImmutableDictionary<EGood, float> goods, ImmutableDictionary<EService, float> services, 
            ImmutableDictionary<EGood, float> goodsW, ImmutableDictionary<EService, float> servicesW)
        {
            GoodsPerPopNeeds = goods;
            ServicesPerPopNeeds = services;
            GoodsPerPopWants = goodsW;
            ServicesPerPopWants = servicesW;
        }
        private static LevelInfo[] CreateLevels()
        {
            var levels = new LevelInfo[LEVELS];
            for (int i = LEVELS - 1; i >= 0; i--)
                levels[i] = new LevelInfo(GetGoodMap(), GetServiceMap(), GetGoodWantMap(), GetServiceWantMap());
            return levels;
        }
        public static LevelInfo GetLevel(int i)
        {
            return levels[i];
        }
        private static ImmutableDictionary<EGood, float> GetGoodMap()
        {
            string[] goodRequirements = FileManager.ReadGoods().Split(';');
            var builder = ImmutableDictionary.CreateBuilder<EGood, float>();
            if (goodRequirements[0].Contains("NONE"))
                return builder.ToImmutable();
            foreach (var requirement in goodRequirements) 
                builder.Add(SplitEntry<EGood>(requirement));
            return builder.ToImmutable();
        }
        private static ImmutableDictionary<EService, float> GetServiceMap()
        {
            string[] serviceRequirements = FileManager.ReadServices().Split(';');
            var builder = ImmutableDictionary.CreateBuilder<EService, float>();
            if (serviceRequirements[0].Contains("NONE"))
                return builder.ToImmutable();
            foreach (var requirement in serviceRequirements)
                builder.Add(SplitEntry<EService>(requirement));
            return builder.ToImmutable();
        }
        private static ImmutableDictionary<EGood, float> GetGoodWantMap()
        {
            string[] goodRequirements = FileManager.ReadGoodWants().Split(';');
            var builder = ImmutableDictionary.CreateBuilder<EGood, float>();
            if (goodRequirements[0].Contains("NONE"))
                return builder.ToImmutable();
            foreach (var requirement in goodRequirements)
                builder.Add(SplitEntry<EGood>(requirement));
            return builder.ToImmutable();
        }
        private static ImmutableDictionary<EService, float> GetServiceWantMap()
        {
            string[] serviceRequirements = FileManager.ReadServiceWants().Split(';');
            var builder = ImmutableDictionary.CreateBuilder<EService, float>();
            if (serviceRequirements[0].Contains("NONE"))
                return builder.ToImmutable();
            foreach (var requirement in serviceRequirements)
                builder.Add(SplitEntry<EService>(requirement));
            return builder.ToImmutable();
        }
        private static KeyValuePair<T, float> SplitEntry<T>(string entry) where T : Enum
        {
            var toks = entry.Split(',');
            return new KeyValuePair<T, float>((T)(object)int.Parse(toks[0]), float.Parse(toks[1]));
        }
        private static class FileManager{
            private static readonly String[] GOODNEEDS = Resources.Load<TextAsset>("GoodLevels").text.Split('\n');
            private static readonly String[] SERVICENEEDS = Resources.Load<TextAsset>("ServiceLevels").text.Split('\n');
            private static readonly String[] GOODWANTS = Resources.Load<TextAsset>("GoodWantLevels").text.Split('\n');
            private static readonly String[] SERVICEWANTS = Resources.Load<TextAsset>("ServiceWantLevels").text.Split('\n');
            private static int idx = 0;
            public static string ReadGoods()
            {
                return GOODNEEDS[idx];
            }
            public static string ReadGoodWants()
            {
                return GOODWANTS[idx];
            }
            public static string ReadServices()
            {
                return SERVICENEEDS[idx];
            }
            public static string ReadServiceWants()
            {
                return SERVICEWANTS[idx++];
            }
        }
    }
}
