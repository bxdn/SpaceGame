using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class LandUnit
    {
        public bool Arable { get; }
        public EResource? Resource { get; }
        public Structure Structure { get; set; } = null;
        public LandUnit(bool arable, EResource? resource)
        {
            if(resource == null)
            {
                resource = ChooseRandomResource();
            }
            Resource = resource;
            Arable = arable;
        }
        public LandUnit(bool arable) : this(arable, null) { }
        public static EResource? ChooseRandomResource()
        {
            int chance = ColonizerR.r.Next(100);
            if(chance < 75)
            {
                return null;
            }
            if(chance < 80)
            {
                return EResource.Metals;
            }
            if (chance < 85)
            {
                return EResource.Gasses;
            }
            if (chance < 90)
            {
                return EResource.EnergySource;
            }
            return EResource.Water;
        }
    }
}
