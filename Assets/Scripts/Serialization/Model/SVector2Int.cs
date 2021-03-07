using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public struct SVector2Int
    {
        public readonly int x;
        public readonly int y;
        public SVector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public SVector2Int(Vector2Int vec)
        {
            this.x = vec.x;
            this.y = vec.y;
        }
    }
}
