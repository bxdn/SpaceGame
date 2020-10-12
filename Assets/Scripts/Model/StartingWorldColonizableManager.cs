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
        private static readonly int MIN_ARABLE = 10;
        private static readonly int MIN_TOTAL = 20;
        public StartingWorldColonizableManager(Orbiter orbiter) : base(orbiter) { }
        public bool DesignateStartingColony()
        {
            if (resources[EResource.ArableLand] < MIN_ARABLE)
                return false;
            if (resources[EResource.Land] < MIN_TOTAL)
                return false;
            if (Resources[EResource.Gasses] == 0)
                return false;
            if (Resources[EResource.Water] == 0)
                return false;
            if (Resources[EResource.Metals] == 0)
                return false;
            if (Resources[EResource.EnergySource] == 0)
                return false;
            Colonize();
            return true;
        }
    }
}
