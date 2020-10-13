using Assets.Scripts.Model;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public interface IColonizableManager
    {
        Domain Owner { get; }
        Colony Colony { get; }
        IDictionary<EResource, int> Resources { get; }
        int HazardFrequency { get; }
    }
}
