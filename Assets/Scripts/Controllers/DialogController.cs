using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogController : EventTrigger
{
    private bool dragging;
    private Vector3 curPos;
    private static readonly IList<GUIScrollable> guis = new List<GUIScrollable>();
    private int scrollVal = 0;

    public void Update()
    {
        if (dragging)
        {
            Vector3 pos = Input.mousePosition - curPos;
            curPos = Input.mousePosition;
            transform.Translate(pos);
        }
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0)
        {
            if (scrollDelta < 0 && scrollVal < guis.Count - 17)
            {
                scrollVal++;
                MoveGUIs(true);
            }
            else if (scrollDelta > 0 && scrollVal > 0)
            {
                scrollVal--;
                MoveGUIs(false);
            }
        }
    }

    private void MoveGUIs(bool ascending)
    {
        foreach (GUIScrollable gui in guis)
        {
            gui.Scroll(ascending);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
            curPos = Input.mousePosition;
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
    }

    public static void Fill(IColonizable colonizable)
    {
        foreach (GUIDestroyable gui in guis)
        {
            gui.Destroy();
        }
        guis.Clear();
        if(colonizable != null)
        {
            Vector2 currentPosition = new Vector2(0, -10);
            foreach (LandUnit unit in colonizable.ColonizableManager.Land)
            {
                guis.Add(new LandUnitGUI(unit, currentPosition));
                currentPosition = new Vector2(0, currentPosition.y - 25);
            }
            currentPosition = new Vector2(0, -10);
            foreach (var good in colonizable.ColonizableManager.Goods)
            {
                guis.Add(new GoodGUI(good.Key, good.Value, currentPosition));
                currentPosition = new Vector2(0, currentPosition.y - 25);
            }
        }
    }
}
