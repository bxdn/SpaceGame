using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public struct SVector2
    {
        public readonly float x;
        public readonly float y;
        public SVector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
