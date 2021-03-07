using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StructureCloseButtonController : EventTrigger
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Constants.COLONY_PANEL.SetActive(true);
            Constants.STRUCTURE_PANEL.SetActive(false);
        }
    }
}
