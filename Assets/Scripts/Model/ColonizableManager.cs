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
        protected readonly IDictionary<EResource, int> resources = new Dictionary<EResource, int>();
        private readonly Area[] features;
        public int Size { get; }
        public IDictionary<EResource, int> Resources
        {
            get => new Dictionary<EResource, int>(resources);
        }
        public Colony CurrentColony { get; private set; }
        private IList<ColonyInfo> colonies = new List<ColonyInfo>();
        public ColonizableManager(Orbiter orbiter)
        { 
            Habitability = ColonizerR.r.Next(100);
            Owner = null;
            features = new Area[orbiter.Size];
            Size = orbiter.Size;
            CalculateResourceLayout(orbiter);
        }
        public void CalculateResourceLayout(Orbiter orbiter)
        {
            foreach (EResource resource in Enum.GetValues(typeof(EResource)))
                resources[resource] = 0;
            for (int i = 0; i < orbiter.Size; i++)
                AddValidResource(i);
        }
        private void AddValidResource(int idx)
        {
            if (ColonizerR.r.Next(100) > Habitability)
                return;
            features[idx] = new Area(EResource.Land);
            if (ColonizerR.r.Next(100) < 5)
                AddResource(idx);
            else
                resources[EResource.Land]++;
        }
        private void AddResource(int idx)
        {
            var resource = allResources[ColonizerR.r.Next(allResources.Length)];
            resources[resource]++;
            features[idx] = new Area(resource);
        }
        public void Colonize(int idx)
        {
            Owner = WorldGeneration.Galaxy.Player.Domain;
            var colony = new Colony(this);
            CurrentColony = colony;
            colonies.Add(new ColonyInfo(colony, idx));
        }
        public Enum GetFeature(int i)
        {
            var feature = features[i];
            if (feature == null)
                return null;
            return feature.Feature;
        }

        public void UpdateFeature(int i, Enum feature)
        {
            features[i] = new Area(feature);
        }

        public void SetCurrentColony(int idx)
        {
            foreach (ColonyInfo info in colonies)
                if (info.Idx == idx)
                    CurrentColony = info.Colony;
        }

        [Serializable]
        private class ColonyInfo
        {
            public Colony Colony { get; }
            public int Idx { get; }
            public ColonyInfo(Colony colony, int idx)
            {
                Colony = colony;
                Idx = idx;
            }
        }
    }
}
