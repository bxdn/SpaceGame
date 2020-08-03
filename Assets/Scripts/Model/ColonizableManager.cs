using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public class ColonizableManager : IColonizableManager
    {
        public Domain Owner { get; set; }
        public int ArableLand { get; }
        public int OtherLand { get; }
        public int HazardFrequency { get; }
        public IDictionary<EResource, int> resources = new Dictionary<EResource, int>();
        public IDictionary<EResource, int> Resources
        {
            get => new Dictionary<EResource, int>(resources);
        }
        private LandUnit[] land;
        public ColonizableManager(Orbiter orbiter)
        {
            HazardFrequency = ColonizerR.r.Next(100);
            if(orbiter is IArable)
            {
                ArableLand = ColonizerR.r.Next(0, orbiter.Size);
            }
            OtherLand = ColonizerR.r.Next(0, orbiter.Size - ArableLand);
            Owner = null;
            CalculateResourceLayout();
        }
        public void CalculateResourceLayout()
        {
            foreach(EResource resource in Enum.GetValues(typeof(EResource)))
            {
                resources[resource] = 0;
            }
            int usableLandTotal = ArableLand + OtherLand;
            land = new LandUnit[usableLandTotal];
            for(int i = 0; i < usableLandTotal; i++)
            {
                LandUnit landUnit = new LandUnit(i < ArableLand);
                land[i] = landUnit;
                if(landUnit.Resource is EResource resource)
                {
                    if (!resources.ContainsKey(resource))
                    {
                        resources[resource] = 0;
                    }
                    resources[resource]++;
                }
            }
        }
    }
}
