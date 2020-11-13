﻿using Assets.Scripts;
using Assets.Scripts.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColonyButtonController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CameraController.Locked)
            Activate();
    }

    private static void Activate()
    {
        Constants.COLONY_PANEL.SetActive(true);
        Constants.GOODS_PANEL.SetActive(true);
        Constants.COLONY_PANEL.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, 0);
        Constants.GOODS_PANEL.GetComponent<RectTransform>().anchoredPosition = new Vector2(300, 0);
        CameraController.Locked = true;
    }
}
