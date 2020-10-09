using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StructurePanelController : EventTrigger
{
    private static readonly IList<GUIScrollable> guis = new List<GUIScrollable>();
    private static LandUnit unit;
    private static StructureInfoGUI infoGUI;
    private bool dragging;
    private Vector3 curPos;
    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Vector3 pos = Input.mousePosition - curPos;
            curPos = Input.mousePosition;
            transform.Translate(pos);
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
    public static void Fill(LandUnit u)
    {
        unit = u;
        foreach (GUIDestroyable gui in guis)
            gui.Destroy();
        guis.Clear();
        if (infoGUI != null)
            infoGUI.Destroy();
        foreach (StructureInfo info in Constants.STRUCTURE_INFOS)
        {
            guis.Add(new StructureGUI(info, unit));
        }
    }
    public static void FillRightSide(StructureInfo info)
    {
        if(infoGUI != null)
            infoGUI.Destroy();
        infoGUI = new StructureInfoGUI(info);
    }
    public static void SetStructure(StructureInfo info)
    {
        unit.Structure = info;
        Constants.STRUCTURE_PANEL.SetActive(false);
        Constants.COLONY_PANEL.SetActive(true);
        if (Selection.CurrentSelection.ModelObject is IColonizable c && c.ColonizableManager is IColonizableManager m && m.Colony is Colony colony)
            SetStructure(info, c, colony);
    }
    private static void SetStructure(StructureInfo info, IColonizable c, Colony colony)
    {
        foreach (var pair in info.Cost)
            colony.IncrementGood(pair.Key, -pair.Value);
        foreach (var pair in info.ServiceFlow)
            colony.IncrementService(pair.Key, pair.Value);
        colony.Workers -= info.RequiredWorkers;
        colony.AddLandUnitWorked(unit);
        ColonyDialogController.Reset(c);
    }
}
