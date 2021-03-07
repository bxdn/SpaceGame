using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColonyCloseButtonController : EventTrigger
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            if(!Constants.COLONY_PANEL.activeSelf && !Constants.GOODS_PANEL.activeSelf)
                CameraController.Locked = false;
        }
    }
}
