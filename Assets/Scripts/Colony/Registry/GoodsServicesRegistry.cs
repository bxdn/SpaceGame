using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Registry
{
    public class GoodsServicesRegistry
    {
        private readonly IDictionary<String, GoodOrService> goods = new Dictionary<String, GoodOrService>();
        private readonly IDictionary<String, GoodOrService> services = new Dictionary<String, GoodOrService>();
        public GoodsServicesRegistry()
        {
            FillMap("Goods", goods);
            FillMap("Services", services);
        }
        private void FillMap(string fileName, IDictionary<String, GoodOrService> map)
        {
            foreach (var good in Resources.Load<TextAsset>(fileName).text.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                map.Add(good, new GoodOrService(good));
        }
        public IEnumerable<GoodOrService> GetAllGoods()
        {
            return goods.Values;
        }
        public IEnumerable<GoodOrService> GetAllServices()
        {
            return services.Values;
        }
        public bool IsService(GoodOrService g)
        {
            return services.ContainsKey(g.Name);
        }
        public GoodOrService Get(String serviceGood)
        {
            if (goods.ContainsKey(serviceGood))
                return goods[serviceGood];
            if (services.ContainsKey(serviceGood))
                return services[serviceGood];
            return null;
        }
        [System.Serializable]
        public class GoodOrService
        {
            public String Name { get; }
            public GoodOrService(String name)
            {
                Name = name;
            }
        }
    }
}
