using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Scripts.Registry.GoodsServicesRegistry;

namespace Assets.Scripts.Interfaces
{
    public interface ILevelInfo
    {
        IDictionary<GoodOrService, float> GoodsServicesPerPopWants { get; }
        IDictionary<GoodOrService, float> GoodsServicesPerPopNeeds { get; }
        int CurrentLevel { get; }
    }
}
