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
        private static ISelectable currentSelection;

        public static void Select(ISelectable newSelection)
        {
            if(newSelection != currentSelection)
            {
                if (currentSelection != null)
                {
                    currentSelection.Deselect();
                }
                newSelection.Select();
                currentSelection = newSelection;
            }
        } 
    }
}
