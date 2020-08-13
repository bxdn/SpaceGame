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
        public static ISelectable CurrentSelection { get; private set; }

        public static void Select(ISelectable newSelection)
        {
            if(newSelection != CurrentSelection)
            {
                if (CurrentSelection != null)
                {
                    CurrentSelection.Deselect();
                }
                newSelection.Select();
                CurrentSelection = newSelection;
            }
        } 
    }
}
