using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class ResourceInfo : ICodable
    {
        public string Name { get; }
        public string Code { get; }
        public ResourceInfo(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
