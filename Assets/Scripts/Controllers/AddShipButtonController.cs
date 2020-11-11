using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddShipButtonController : EventTrigger
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Selection.CurrentSelection.ModelObject is IColonizable c &&
            c.ColonizableManager is IColonizableManager m && Validate(m.Colony))
                m.Colony.AddRocket();
        }
    }

    private bool Validate(Colony colony)
    {
        return colony.Goods.ContainsKey(EGood.BuildingMaterials) && colony.Goods[EGood.BuildingMaterials].Value >= 50
            && colony.Goods.ContainsKey(EGood.Hydrogen) && colony.Goods[EGood.Hydrogen].Value >= 100
            && colony.Goods.ContainsKey(EGood.Energy) && colony.Goods[EGood.Energy].Value >= 50
            && colony.Goods.ContainsKey(EGood.Oxygen) && colony.Goods[EGood.Oxygen].Value >= 50
            && colony.Goods.ContainsKey(EGood.Tools) && colony.Goods[EGood.Tools].Value >= 50;
    }
}
