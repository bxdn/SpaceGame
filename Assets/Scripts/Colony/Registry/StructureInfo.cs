using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Scripts.Registry.GoodsServicesRegistry;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class StructureInfo : ICodable
    {
        public string Name { get; }
        public ImmutableDictionary<string, float> GoodCost { get; }
        public ImmutableDictionary<string, float> Flow { get; }
        public ImmutableDictionary<string, float> ServiceFlow { get; }
        public string PrereqFeature { get; }
        public int WorkerLevel { get; }
        public string Code { get; }
        public StructureInfo(String name, ImmutableDictionary<string, float> cost, ImmutableDictionary<string, float> flow, 
            ImmutableDictionary<string, float> serviceFlow, string prereqFeature, int workerLevel, string code)
        {
            this.Name = name;
            this.GoodCost = cost;
            this.Flow = flow;
            this.PrereqFeature = prereqFeature;
            this.ServiceFlow = serviceFlow;
            this.WorkerLevel = workerLevel;
            this.Code = code;
        }
    }
}
