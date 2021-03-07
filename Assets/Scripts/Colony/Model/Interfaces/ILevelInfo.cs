using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface ILevelInfo
    {
        IDictionary<EService, float> ServicesPerPopWants{get;}
        IDictionary<EGood, float> GoodsPerPopWants { get; }
        IDictionary<EService, float> ServicesPerPopNeeds { get; }
        IDictionary<EGood, float> GoodsPerPopNeeds { get; }
        int CurrentLevel { get; }
    }
}
