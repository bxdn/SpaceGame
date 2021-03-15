using Assets.Scripts.Model;
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public interface IColonizableManager
    {
        Domain Owner { get; }
        Colony CurrentColony { get; }
        int Habitability { get; }
        Enum GetFeature(int i);
        void UpdateFeature(int i, Enum feature);
        int Size { get; }
        void Colonize(int idx);
        void SetCurrentColony(int idx);
    }
}
