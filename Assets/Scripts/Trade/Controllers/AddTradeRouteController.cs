using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using Assets.Scripts.Registry;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers
{
    class AddTradeRouteController : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            var trade = RegistryUtil.Structures.GetStructure("Local Trade Depot");
            if((Selection.CurrentSelection as IColonizable).ColonizableManager.CurrentColony is Colony c
                && c.Structures.ContainsKey(trade)
                && c.Structures[trade].Count > 0)
                TradePanelController.AddNewRoute();
        }
    }
}
