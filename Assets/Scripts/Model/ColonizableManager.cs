using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class ColonizableManager : IColonizableManager
    {
        private static readonly EResource[] allResources = (Enum.GetValues(typeof(EResource)) as EResource[]).ToArray();
        public Domain Owner { get; private set; }
        public int Habitability { get; }
        protected readonly IDictionary<EResource, IList<int>> resources = new Dictionary<EResource, IList<int>>();
        private readonly Enum[] features;
        public int Size { get; }
        public IDictionary<EResource, IList<int>> Resources
        {
            get => new Dictionary<EResource, IList<int>>(resources);
        }
        public Colony Colony { get; private set; }
        public ColonizableManager(Orbiter orbiter)
        {
            Habitability = ColonizerR.r.Next(100);
            Owner = null;
            features = new Enum[orbiter.Size];
            Size = orbiter.Size;
            CalculateResourceLayout(orbiter);
        }
        public void CalculateResourceLayout(Orbiter orbiter)
        {
            foreach (EResource resource in Enum.GetValues(typeof(EResource)))
                resources[resource] = new List<int>();
            for (int i = 0; i < orbiter.Size; i++)
                AddValidResource(i);
        }
        private void AddValidResource(int idx)
        {
            if (ColonizerR.r.Next(100) > Habitability)
                return;
            resources[EResource.Land].Add(idx);
            features[idx] = EResource.Land;
            if (ColonizerR.r.Next(100) < 5)
                AddResource(idx);
        }
        private void AddResource(int idx)
        {
            var resource = allResources[ColonizerR.r.Next(allResources.Length)];
            resources[resource].Add(idx);
            features[idx] = resource;
        }
        public void Colonize(Galaxy g)
        {
            Owner = g.Player.Domain;
            Colony = new Colony(g, Resources);
        }
        public Enum GetFeature(int i)
        {
            return features[i];
        }

        public void UpdateFeature(int i, Enum feature)
        {
            features[i] = feature;
        }
    }
}
