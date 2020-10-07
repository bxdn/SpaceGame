﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface ISelectable
    {
        void Select();
        void Deselect();
        IModelObject ModelObject { get; }
    }
}
