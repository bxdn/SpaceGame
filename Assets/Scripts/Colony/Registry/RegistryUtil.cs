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
        public readonly static string HOUSING = "Housing";
        public readonly static string HQ = "HQ";
        public readonly static string CHIPS = "Chips";
        public readonly static string STEEL = "Steel";
        public readonly static string ENERGY = "Energy";
        public readonly static string ALCOHOL = "Alcohol";
        public readonly static string FOOD = "Food";
        public readonly static string WATER = "Water";

        public static GoodsServicesRegistry GoodsServices { get; } = new GoodsServicesRegistry();
        public static StructureRegistry Structures { get; } = new StructureRegistry();
        public static ResourceRegistry Resources { get; } = new ResourceRegistry();
    }
}
