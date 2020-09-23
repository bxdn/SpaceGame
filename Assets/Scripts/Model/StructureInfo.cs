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
        public ImmutableDictionary<EGood, int> Cost { get; }
        public ImmutableDictionary<EGood, float> Flow { get; }
        public ImmutableDictionary<EService, float> ServiceFlow { get; }
        public int Capacity { get; }
        public StructureInfo(String name, ImmutableDictionary<EGood, int> cost, ImmutableDictionary<EGood, float> flow, 
            ImmutableDictionary<EService, float> serviceFlow, int capacity)
        {
            this.Name = name;
            this.Cost = cost;
            this.Flow = flow;
            this.Capacity = capacity;
            this.ServiceFlow = serviceFlow;
        }
    }
}
