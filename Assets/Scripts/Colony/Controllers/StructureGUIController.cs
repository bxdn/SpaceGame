using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class StructureGUIController : EventTrigger
{
    public EStructure Structure { get; set; }
    private float clickTime = -1;

    public override void OnPointerDown(PointerEventData eventData)
    {
        float newClickTime = Time.time;
        Console.WriteLine(clickTime.ToString(), newClickTime);
        if (newClickTime - clickTime < 0.3f && Validate())
            StructurePanelController.SetStructure(Structure);
        else
        {
            StructurePanelController.FillRightSide((StructureInfo)Constants.FEATURE_MAP[Structure]);
            clickTime = newClickTime;
        }
    }
    private bool Validate()
    {
        return Selection.CurrentSelection is IColonizable c &&
            c.ColonizableManager != null &&
            ValidateColony(c.ColonizableManager.CurrentColony, Structure);
    }
    public static bool ValidateColony(Colony colony, EStructure structure)
    {
        return true;
        var info = (StructureInfo) Constants.FEATURE_MAP[structure];
        if (info.WorkerLevel > colony.Level.CurrentLevel)
            return false;
        if (structure == EStructure.Housing && !colony.CanBeSettled())
            return false;
        var toRet = true;
        var enumerator = info.GoodCost.GetEnumerator();
        enumerator.Reset();
        KeyValuePair<EGood, int> current;
        while (toRet && enumerator.MoveNext())
                toRet &= colony.Goods.ContainsKey((current = enumerator.Current).Key) && colony.Goods[current.Key].Value >= current.Value;
        return toRet;
    }
}
