using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddStructureButtonController : EventTrigger
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Constants.STRUCTURE_PANEL.SetActive(true);
            Constants.COLONY_PANEL.SetActive(false);
            if (Selection.CurrentSelection is IColonizable c &&
            c.ColonizableManager is IColonizableManager m)
                StructurePanelController.Fill(m.CurrentColony);
        }
    }
}
