using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StructureGUIController : EventTrigger
{
    public StructureInfo Structure { get; set; }
    private float clickTime = -1;

    public override void OnPointerDown(PointerEventData eventData)
    {
        float newClickTime = Time.time;
        Console.WriteLine(clickTime.ToString(), newClickTime);
        if (newClickTime - clickTime < 0.3f && Validate())
            StructurePanelController.SetStructure(Structure);
        else
        {
            StructurePanelController.FillRightSide(Structure);
            clickTime = newClickTime;
        }
    }
    private bool Validate()
    {
        return (Selection.CurrentSelection as IColonizable).ColonizableManager.CurrentColony.CanBuildStructure(Structure);
    }
}
