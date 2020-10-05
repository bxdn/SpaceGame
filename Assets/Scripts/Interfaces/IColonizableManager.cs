using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public interface IColonizableManager
    {
        Domain Owner { get; }
        Colony Colony { get; }
        IDictionary<EResource, int> Resources { get; }
        int ArableLand { get; }
        int OtherLand { get; }
        int HazardFrequency { get; }
        LandUnit[] Land { get; }
    }
}
