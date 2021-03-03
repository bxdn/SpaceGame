using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public class ResourceRegistry
    {
        public static ResourceInfo Land { get; private set; } = GetLand();
        public static ResourceInfo Water { get; private set; } = GetWater();
        public static ResourceInfo Silicon { get; private set; } = GetSilicon();

        public static ResourceInfo Iron { get; private set; } = GetIron();

        private static ResourceInfo GetLand()
        {
            return new ResourceInfo("Land", "");
        }
        private static ResourceInfo GetWater()
        {
            return new ResourceInfo("Water", "W");
        }
        private static ResourceInfo GetSilicon()
        {
            return new ResourceInfo("Silicon", "S");
        }
        private static ResourceInfo GetIron()
        {
            return new ResourceInfo("Iron", "I");
        }
    }
}
