using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeRoutesButtonController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        TradePanelController.Fill((Selection.CurrentSelection as IColonizable).ColonizableManager.CurrentColony);
        Constants.TRADE_PANEL.SetActive(true);
    }
}
