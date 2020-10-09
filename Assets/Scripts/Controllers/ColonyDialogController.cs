using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColonyDialogController : EventTrigger
{
    private bool dragging;
    private Vector3 curPos;
    private static readonly IList<GUIScrollable> guis = new List<GUIScrollable>();
    private static int scrollVal = 0;

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
            if (scrollDelta < 0 && scrollVal < guis.Count - 20)
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

    private static void MoveGUIs(bool ascending)
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

    public static void Reset(IColonizable colonizable)
    {
        scrollVal = 0;
        if (colonizable != null)
            Fill(colonizable);
    }
    public static void Update(IColonizable colonizable)
    {
        Fill(colonizable);
        for (int i = 0; i < scrollVal; i++)
            MoveGUIs(true);
    }
    private static void Fill(IColonizable colonizable)
    {
        foreach (GUIDestroyable gui in guis)
            gui.Destroy();
        guis.Clear();
        Vector2 currentPosition = new Vector2(0, 0);
        foreach (LandUnit unit in colonizable.ColonizableManager.Land)
        {
            guis.Add(new LandUnitGUI(unit, currentPosition));
            currentPosition = new Vector2(0, currentPosition.y - 25);
        }
        currentPosition = new Vector2(0, 0);
        foreach (var good in colonizable.ColonizableManager.Colony.Goods)
        {
            guis.Add(new GoodServiceGUI(good.Key, good.Value, currentPosition));
            currentPosition = new Vector2(0, currentPosition.y - 25);
        }
        foreach (var service in colonizable.ColonizableManager.Colony.Services)
        {
            guis.Add(new GoodServiceGUI(service.Key, service.Value, currentPosition));
            currentPosition = new Vector2(0, currentPosition.y - 25);
        }
        Constants.WORK_VAL.text = colonizable.ColonizableManager.Colony.Workers.ToString();
        Constants.POP_VAL.text = colonizable.ColonizableManager.Colony.Population.ToString();
    }
}
