using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers
{
    class AddTradeRouteController : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if((Selection.CurrentSelection as IColonizable).ColonizableManager.CurrentColony is Colony c
                && c.Structures.ContainsKey(EStructure.LocalTradeDepot)
                && c.Structures[EStructure.LocalTradeDepot].Count > 0)
                TradePanelController.AddNewRoute();
        }
    }
}
