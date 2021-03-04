using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    class GoodsStructure
    {
        public EStructure Structure { get; }
        public float DistanceFromHub { get; private set; }
        public GoodsStructure(EStructure structure, float distance)
        {
            Structure = structure;
            DistanceFromHub = distance;
        }
    }
}
