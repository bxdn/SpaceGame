using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class StructureInfo
    {
        public string Name { get; }
        public ImmutableDictionary<EGood, int> GoodCost { get; }
        public ImmutableDictionary<EResource, int> ResourceCost { get; }
        public ImmutableDictionary<EGood, float> Flow { get; }
        public ImmutableDictionary<EService, float> ServiceFlow { get; }
        public Enum PrereqFeature { get; }
        public int WorkerLevel { get; }
        public StructureInfo(String name, ImmutableDictionary<EGood, int> cost, ImmutableDictionary<EResource, int> resourceCost, ImmutableDictionary<EGood, float> flow, 
            ImmutableDictionary<EService, float> serviceFlow, Enum prereqFeature, int workerLevel)
        {
            this.Name = name;
            this.GoodCost = cost;
            this.ResourceCost = resourceCost;
            this.Flow = flow;
            this.PrereqFeature = prereqFeature;
            this.ServiceFlow = serviceFlow;
            this.WorkerLevel = workerLevel;
        }
    }
}
