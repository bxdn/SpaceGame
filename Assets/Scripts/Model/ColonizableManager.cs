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
        public int HazardFrequency { get; }
        protected readonly IDictionary<EResource, int> resources = new Dictionary<EResource, int>();
        public IDictionary<EResource, int> Resources
        {
            get => new Dictionary<EResource, int>(resources);
        }
        public Colony Colony { get; private set; }
        public ColonizableManager(Orbiter orbiter)
        {
            HazardFrequency = ColonizerR.r.Next(100);
            Owner = null;
            CalculateResourceLayout(orbiter);
        }
        public void CalculateResourceLayout(Orbiter orbiter)
        {
            foreach (EResource resource in Enum.GetValues(typeof(EResource)))
                resources[resource] = 0;
            if (orbiter is IArable)
                resources[EResource.ArableLand] = ColonizerR.r.Next(0, orbiter.Size);
            int nonArableLand = ColonizerR.r.Next(0, orbiter.Size - resources[EResource.ArableLand]);
            resources[EResource.Land] = resources[EResource.ArableLand] + nonArableLand;
            for (int i = 0; i < resources[EResource.Land]; i++)
                AddValidResource();
        }
        private void AddValidResource()
        {
            var allResources = Enum.GetValues(typeof(EResource)) as EResource[];
            if (ColonizerR.r.Next(100) > 33)
                resources[allResources[ColonizerR.r.Next(allResources.Length)]]++;
        }
        public void Colonize()
        {
            Owner = Player.Domain;
            Colony = new Colony(Resources);
        }
    }
}
