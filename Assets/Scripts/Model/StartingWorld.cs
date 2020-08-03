using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class StartingWorld : Orbiter, IColonizable
    {
        public override int Size => 30;

        public override string Name { get; set; }

        public override string Type => throw new NotImplementedException();

        public IColonizableManager ColonizableManager => throw new NotImplementedException();

        public StartingWorld(IOrbitParent parent) : base(parent) { }
    }
}
