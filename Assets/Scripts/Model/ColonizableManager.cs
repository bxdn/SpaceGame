using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public class ColonizableManager : IColonizableManager
    {
        private readonly Orbiter orbiter;
        public Domain Owner { get; set; }
        public int ArableLand { get; set; }
        public int OtherLand { get; set; }
        public int HazardFrequency { get; set; }
        public ColonizableManager(Orbiter orbiter)
        {
            this.orbiter = orbiter;
            HazardFrequency = ColonizerR.r.Next(100);
            Owner = null;
        }
        public void CalculateLandDivision()
        {
            ArableLand = ColonizerR.r.Next(0, orbiter.Size);
            OtherLand = ColonizerR.r.Next(0, ArableLand);
        }
    }
}
