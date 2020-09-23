using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class Structure
    {
        public StructureInfo Info { get; }
        public int CapacityFilled { get; set; }
        public Structure(StructureInfo info)
        {
            Info = info;
        }
    }
}
