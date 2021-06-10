using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class Area
    {
        public ICodable Feature { get; }
        public Area(ICodable feature)
        {
            Feature = feature;
        }
    }
}
