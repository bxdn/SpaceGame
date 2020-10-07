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
    public StructureInfo Info { get; set; }
    public LandUnit Unit { get; set; }
    private float clickTime = -1;

    public override void OnPointerDown(PointerEventData eventData)
    {
        float newClickTime = Time.time;
        Console.WriteLine(clickTime.ToString(), newClickTime);
        if (newClickTime - clickTime < 0.3f && Validate())
            StructurePanelController.SetStructure(Info);
        else
        {
            StructurePanelController.FillRightSide(Info);
            clickTime = newClickTime;
        }
    }
    private bool Validate()
    {
        return Selection.CurrentSelection.ModelObject is IColonizable c &&
            c.ColonizableManager != null &&
            ValidateColony(c.ColonizableManager.Colony);
    }
    private bool ValidateColony(Colony colony)
    {
        if(colony.Workers < Info.RequiredWorkers)
            return false;
        if (!Info.ValidationFunction.Invoke(Unit))
            return false;
        var toRet = true;
        var enumerator = Info.Cost.GetEnumerator();
        enumerator.Reset();
        KeyValuePair<EGood, int> current;
        while (toRet && enumerator.MoveNext())
                toRet &= colony.Goods.ContainsKey((current = enumerator.Current).Key) && colony.Goods[current.Key] >= current.Value;
        return toRet;
    }
}
