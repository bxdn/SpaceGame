using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public class LevelInfo
    {
        public ImmutableDictionary<EGood, float> GoodsPerPop { get; }
        public ImmutableDictionary<EService, float> ServicesPerPop { get; }
        public LevelInfo Next { get; }
        public int Idx { get; }
        private static readonly int LEVELS = 5;
        public static LevelInfo FirstLevel { get; } = CreateLevels();
        public LevelInfo(ImmutableDictionary<EGood, float> goods, ImmutableDictionary<EService, float> services, LevelInfo next, int idx)
        {
            Idx = idx;
            GoodsPerPop = goods;
            ServicesPerPop = services;
            Next = next;
        }
        private static LevelInfo CreateLevels()
        {
            LevelInfo current = null;
            for (int i = LEVELS; i >= 0; i--)
                current = new LevelInfo(GetGoodMap(), GetServiceMap(), current, i);
            return current;
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
            private static readonly string GOOD_PATH = "Assets/Resources/GoodLevels.txt";
            private static readonly string SERVICE_PATH = "Assets/Resources/ServiceLevels.txt";
            private static readonly StreamReader GOOD_READER = new StreamReader(GOOD_PATH);
            private static readonly StreamReader SERVICE_READER = new StreamReader(SERVICE_PATH);
            public static string ReadGoods()
            {
                return GOOD_READER.ReadLine();
            }
            public static string ReadServices()
            {
                return SERVICE_READER.ReadLine();
            }
        }
    }
}
