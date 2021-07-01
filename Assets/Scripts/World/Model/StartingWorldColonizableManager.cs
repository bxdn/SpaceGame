using Assets.Scripts.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class StartingWorldColonizableManager : ColonizableManager
    {
        public StartingWorldColonizableManager(Orbiter orbiter) : base(orbiter) { }
        public bool DesignateStartingColony(Galaxy g)
        {
            if (Habitability < 90)
                return false;
            if (getResourceCount("Water") < 10)
                return false;
            if (getResourceCount("Iron") < 1)
                return false;
            if (getResourceCount("Silicon") < 1)
                return false;
            if (getResourceCount("Copper") < 1)
                return false;
            return true;
        }
        private int getResourceCount(string resource)
        {
            var info = RegistryUtil.Resources.Get(resource);
            return featureMap.ContainsKey(info) ? featureMap[info] : 0;
        }
    }
}
