using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public interface IColonizableManager
    {
        Domain Owner { get; }
        Colony CurrentColony { get; }
        IEnumerable<ColonyInfo> Colonies { get; }
        int Habitability { get; }
        ICodable GetFeature(int i);
        void UpdateFeature(int i, ICodable feature);
        int Size { get; }
        void Colonize(int idx);
        void SetCurrentColony(int idx);
    }
}
