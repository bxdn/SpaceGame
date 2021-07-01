using Assets.Scripts.Interfaces;
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
        public int Size { get; }
        public Colony CurrentColony { get; private set; }
        public IEnumerable<ColonyInfo> Colonies { get; private set; } = new List<ColonyInfo>();
        private readonly ICodable[] features;
        protected readonly IDictionary<ICodable, int> featureMap = new Dictionary<ICodable, int>();
        public ColonizableManager(Orbiter orbiter)
        { 
            Habitability = ColonizerR.r.Next(100);
            Owner = null;
            var resourceManager = new ResourceLayoutManager(orbiter.Size, ColonizerR.r.Next(), Habitability);
            resourceManager.LayoutResources();
            features = resourceManager.Fields;
            featureMap = resourceManager.FieldMap;
            Size = orbiter.Size;
        }
        public void Colonize(int idx)
        {
            Owner = WorldGeneration.Galaxy.Player.Domain;
            var colony = new Colony(this, idx);
            CurrentColony = colony;
            (Colonies as List<ColonyInfo>).Add(new ColonyInfo(colony, idx));
        }
        public ICodable GetFeature(int i)
        {
            return features[i];
        }

        public void UpdateFeature(int i, ICodable feature)
        {
            features[i] = feature;
        }

        public void SetCurrentColony(int idx)
        {
            foreach (ColonyInfo info in Colonies)
                if (info.Idx == idx)
                    CurrentColony = info.Colony;
        }
    }
}
