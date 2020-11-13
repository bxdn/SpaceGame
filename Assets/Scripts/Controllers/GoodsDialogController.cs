using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoodsDialogController : EventTrigger
{
    private bool dragging;
    private Vector3 curPos;
    private static readonly IList<GUIScrollable> goodGuis = new List<GUIScrollable>();
    private static int goodScrollVal = 0;

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
            if (scrollDelta < 0 && goodScrollVal < goodGuis.Count - 13)
            {
                goodScrollVal++;
                MoveStructGUIs(true);
            }
            else if (scrollDelta > 0 && goodScrollVal > 0)
            {
                goodScrollVal--;
                MoveStructGUIs(false);
            }
        }
    }
    private static void MoveStructGUIs(bool ascending)
    {
        foreach (GUIScrollable gui in goodGuis)
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
        goodScrollVal = 0;
        if (colonizable != null)
            Fill(colonizable);
    }
    public static void Update(IColonizable colonizable)
    {
        Fill(colonizable);
        for (int i = 0; i < goodScrollVal; i++)
            MoveStructGUIs(true);
    }
    private static void Fill(IColonizable colonizable)
    {
        foreach (GUIDestroyable gui in goodGuis)
            gui.Destroy();
        goodGuis.Clear();
        int population = colonizable.ColonizableManager.Colony.Population;
        Vector2 currentPosition = new Vector2(0, 0);
        foreach (var good in colonizable.ColonizableManager.Colony.Goods)
        {
            goodGuis.Add(new GoodServiceGUI(good.Key, good.Value, currentPosition));
            currentPosition = new Vector2(0, currentPosition.y - 25);
        }
        foreach (var service in colonizable.ColonizableManager.Colony.Services)
        {
            goodGuis.Add(new GoodServiceGUI(service.Key, service.Value, currentPosition));
            currentPosition = new Vector2(0, currentPosition.y - 25);
        }
        foreach (var resource in colonizable.ColonizableManager.Colony.Resources)
        {
            goodGuis.Add(new GoodServiceGUI(resource.Key, resource.Value, currentPosition));
            currentPosition = new Vector2(0, currentPosition.y - 25);
        }
        currentPosition = new Vector2(300, 0);
        foreach (var good in LevelInfo.GetLevel(colonizable.ColonizableManager.Colony.CurrentLevel).GoodsPerPopNeeds)
        {
            goodGuis.Add(new DemandGUI(good.Key, good.Value * population / 100f, currentPosition));
            currentPosition = new Vector2(300, currentPosition.y - 25);
        }
        foreach (var service in LevelInfo.GetLevel(colonizable.ColonizableManager.Colony.CurrentLevel).ServicesPerPopNeeds)
        {
            goodGuis.Add(new DemandGUI(service.Key, service.Value * population, currentPosition));
            currentPosition = new Vector2(300, currentPosition.y - 25);
        }
        currentPosition = new Vector2(450, 0);
        foreach (var good in LevelInfo.GetLevel(colonizable.ColonizableManager.Colony.CurrentLevel).GoodsPerPopWants)
        {
            goodGuis.Add(new DemandGUI(good.Key, good.Value * population / 100f, currentPosition));
            currentPosition = new Vector2(450, currentPosition.y - 25);
        }
        foreach (var service in LevelInfo.GetLevel(colonizable.ColonizableManager.Colony.CurrentLevel).ServicesPerPopWants)
        {
            goodGuis.Add(new DemandGUI(service.Key, service.Value * population, currentPosition));
            currentPosition = new Vector2(450, currentPosition.y - 25);
        }
        Constants.WORK_VAL.text = colonizable.ColonizableManager.Colony.Workers.ToString();
        Constants.POP_VAL.text = population.ToString();
        Constants.INF_VAL.text = colonizable.ColonizableManager.Colony.Influence.ToString();
        Constants.LVL_VAL.text = colonizable.ColonizableManager.Colony.CurrentLevel.ToString();
    }
}
