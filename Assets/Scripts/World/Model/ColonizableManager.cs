using Assets.Scripts.World.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class ColonizableManager : IColonizableManager
    {
        public Domain Owner { get; private set; }
        public int Habitability { get; }
        protected readonly IDictionary<EResource, int> resources = new Dictionary<EResource, int>();
        public int Size { get; }
        public Colony CurrentColony { get; private set; }
        public IEnumerable<ColonyInfo> Colonies { get; private set; } = new List<ColonyInfo>();
        private readonly Area[] features;
        public ColonizableManager(Orbiter orbiter)
        { 
            Habitability = ColonizerR.r.Next(100);
            Owner = null;
            var resourceManager = new ResourceLayoutManager(orbiter.Size, ColonizerR.r.Next(), Habitability);
            features = resourceManager.LayoutResources();
            Size = orbiter.Size;
        }
        public void Colonize(int idx)
        {
            Owner = WorldGeneration.Galaxy.Player.Domain;
            var colony = new Colony(this, idx);
            CurrentColony = colony;
            (Colonies as List<ColonyInfo>).Add(new ColonyInfo(colony, idx));
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
            foreach (ColonyInfo info in Colonies)
                if (info.Idx == idx)
                    CurrentColony = info.Colony;
        }
    }
}
