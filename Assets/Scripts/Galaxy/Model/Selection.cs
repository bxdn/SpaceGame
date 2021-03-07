using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Controllers
{
    public static class Selection
    {
        public static IModelObject CurrentSelection { get; private set; }

        public static void Select(IModelObject newSelection)
        {
            if(newSelection != CurrentSelection)
            {
                CurrentSelection = newSelection;
            }
        } 
    }
}
