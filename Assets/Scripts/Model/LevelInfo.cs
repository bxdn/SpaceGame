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
        public ImmutableDictionary<EGood, float> GoodsPerPop { get; }
        public ImmutableDictionary<EService, float> ServicesPerPop { get; }
        public int Idx { get; }
        private static readonly int LEVELS = 6;
        private static readonly LevelInfo[] levels = CreateLevels();
        public LevelInfo(ImmutableDictionary<EGood, float> goods, ImmutableDictionary<EService, float> services)
        {
            GoodsPerPop = goods;
            ServicesPerPop = services;
        }
        private static LevelInfo[] CreateLevels()
        {
            var levels = new LevelInfo[LEVELS];
            for (int i = LEVELS - 1; i >= 0; i--)
                levels[i] = new LevelInfo(GetGoodMap(), GetServiceMap());
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
            foreach(var requirement in goodRequirements) 
                builder.Add(SplitEntry<EGood>(requirement));
            return builder.ToImmutable();
        }
        private static ImmutableDictionary<EService, float> GetServiceMap()
        {
            string[] serviceRequirements = FileManager.ReadServices().Split(';');
            var builder = ImmutableDictionary.CreateBuilder<EService, float>();
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
            private static readonly String[] GOODDEMAND = Resources.Load<TextAsset>("GoodLevels").text.Split('\n');
            private static readonly String[] SERVICEDEMAND = Resources.Load<TextAsset>("ServiceLevels").text.Split('\n');
            private static int idx = 0;
            public static string ReadGoods()
            {
                return GOODDEMAND[idx];
            }
            public static string ReadServices()
            {
                return SERVICEDEMAND[idx++];
            }
        }
    }
}
