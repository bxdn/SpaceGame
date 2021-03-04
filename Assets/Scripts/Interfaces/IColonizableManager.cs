using Assets.Scripts.Model;
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public interface IColonizableManager
    {
        Domain Owner { get; }
        Colony Colony { get; }
        IDictionary<EResource, int> Resources { get; }
        int Habitability { get; }
        Enum GetFeature(int i);
        void UpdateFeature(int i, Enum feature);
        int Size { get; }
        void Colonize();
    }
}
