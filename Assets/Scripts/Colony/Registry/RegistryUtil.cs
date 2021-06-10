using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Registry
{
    public static class RegistryUtil
    {
        public static GoodsServicesRegistry GoodsServices { get; } = new GoodsServicesRegistry();
        public static StructureRegistry Structures { get; } = new StructureRegistry();
        public static ResourceRegistry Resources { get; } = new ResourceRegistry();
    }
}
