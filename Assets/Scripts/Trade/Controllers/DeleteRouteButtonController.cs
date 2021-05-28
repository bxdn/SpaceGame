using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Trade.Model;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Trade.Controllers
{
    class DeleteRouteButtonController : MonoBehaviour, IPointerClickHandler
    {
        private TradeRoute tradeRoute;
        public void Init(TradeRoute tradeRoute)
        {
            this.tradeRoute = tradeRoute;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            tradeRoute.Originator.TradeManager.RemoveRoute(tradeRoute);
            tradeRoute.Receiver.TradeManager.RemoveRoute(tradeRoute);
            TradePanelController.Reset((Selection.CurrentSelection as IColonizable).ColonizableManager.CurrentColony);
        }
    }
}
