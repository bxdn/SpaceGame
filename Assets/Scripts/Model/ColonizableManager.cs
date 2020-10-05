using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class ColonizableManager : IColonizableManager
    {
        public Domain Owner { get; private set; }
        public int ArableLand { get; }
        public int OtherLand { get; }
        public int HazardFrequency { get; }
        private readonly IDictionary<EResource, int> resources = new Dictionary<EResource, int>();
        public IDictionary<EResource, int> Resources
        {
            get => new Dictionary<EResource, int>(resources);
        }
        public Colony Colony { get; private set; }
        public LandUnit[] Land { get; private set; }
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
            Land = new LandUnit[usableLandTotal];
            for(int i = 0; i < usableLandTotal; i++)
            {
                LandUnit landUnit = new LandUnit(i < ArableLand);
                Land[i] = landUnit;
                if(landUnit.Resource is EResource resource)
                {
                    resources[resource]++;
                }
            }
        }
        public void Colonize()
        {
            Owner = Player.Domain;
            Colony = new Colony();
        }
    }
}
