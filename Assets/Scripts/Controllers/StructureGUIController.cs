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
            StructurePanelController.FillRightSide(Constants.STRUCTURE_MAP[Structure]);
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
        StructureInfo info = Constants.STRUCTURE_MAP[Structure];
        if (colony.Workers < info.RequiredWorkers || info.WorkerLevel > colony.CurrentLevel)
            return false;
        var toRet = true;
        var enumerator = info.GoodCost.GetEnumerator();
        enumerator.Reset();
        KeyValuePair<EGood, int> current;
        while (toRet && enumerator.MoveNext())
                toRet &= colony.Goods.ContainsKey((current = enumerator.Current).Key) && colony.Goods[current.Key].Value >= current.Value;
        var resourceEnumerator = info.ResourceCost.GetEnumerator();
        resourceEnumerator.Reset();
        KeyValuePair<EResource, int> currentResource;
        while (toRet && resourceEnumerator.MoveNext())
            toRet &= colony.Resources.ContainsKey((currentResource = resourceEnumerator.Current).Key) && colony.Resources[currentResource.Key] >= currentResource.Value;
        return toRet;
    }
}
