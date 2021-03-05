using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class Structure
    {
        public SVector2Int Location { get; }
        public float Multiplier { get; private set; }
        public Structure(SVector2Int location, float multiplier)
        {
            Location = location;
            Multiplier = multiplier;
        }
    }
}
