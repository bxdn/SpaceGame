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
        private static readonly int MIN_TOTAL = 500;
        public StartingWorldColonizableManager(Orbiter orbiter) : base(orbiter) { }
        public bool DesignateStartingColony(Galaxy g)
        {
            if (Habitability < 90)
                return false;
            if (resources[EResource.Land] < MIN_TOTAL)
                return false;
            if (Resources[EResource.Water] < 50)
                return false;
            if (Resources[EResource.Iron] < 50)
                return false;
            Colonize(g);
            return true;
        }
    }
}
