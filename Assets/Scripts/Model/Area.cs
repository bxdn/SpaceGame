using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    class Area
    {
        public Enum Feature { get; }
        public Area(Enum feature)
        {
            Feature = feature;
        }
    }
}
