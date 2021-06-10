using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class ResourceRegistry
    {
        private readonly IDictionary<String, ResourceInfo> resources = new Dictionary<String, ResourceInfo>(); 

        public ResourceRegistry()
        {
            foreach (var resource in Resources.Load<TextAsset>("Resources").text.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                AddResource(resource);
        }
        private void AddResource(string resource)
        {
            var data = resource.Split(',');
            resources.Add(data[0], new ResourceInfo(data[0], data[1]));
        }
        public ResourceInfo Get(string name)
        {
            if (resources.ContainsKey(name))
                return resources[name];
            return null;
        }
        public IEnumerable<ResourceInfo> GetAllResources()
        {
            return resources.Values;
        }
    }
}
