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
        public bool DesignateStartingColony()
        {
            if (ArableLand < 10)
            {
                return false;
            }
            if (OtherLand < 10)
            {
                return false;
            }
            if (Resources[EResource.Gasses] == 0)
            {
                return false;
            }
            if (Resources[EResource.Water] == 0)
            {
                return false;
            }
            if (Resources[EResource.Metals] == 0)
            {
                return false;
            }
            if (Resources[EResource.EnergySource] == 0)
            {
                return false;
            }
            Colonize();
            return true;
        }
    }
}
