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
/*            if (Resources[EResource.Water] < 10)
                return false;
            if (Resources[EResource.Iron] < 10)
                return false;
            if (Resources[EResource.Copper] < 10)
                return false;
            if (Resources[EResource.Silicon] < 10)
                return false;*/
            return true;
        }
    }
}
