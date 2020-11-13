﻿using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class StructurePanelController : EventTrigger
{
    private static readonly IList<GUIScrollable> guis = new List<GUIScrollable>();
    private static StructureInfoGUI infoGUI;
    private bool dragging;
    private Vector3 curPos;
    private static Colony colony;
    private static int scrollVal = 0;
    // Update is called once per frame
    void Update()
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
            if (scrollDelta < 0 && scrollVal < guis.Count - 13)
            {
                scrollVal++;
                MoveStructGUIs(true);
            }
            else if (scrollDelta > 0 && scrollVal > 0)
            {
                scrollVal--;
                MoveStructGUIs(false);
            }
        }
    }
    private static void MoveStructGUIs(bool ascending)
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
    public static void Fill(Colony c)
    {
        scrollVal = 0;
        colony = c;
        foreach (GUIDestroyable gui in guis)
            gui.Destroy();
        guis.Clear();
        if (infoGUI != null)
            infoGUI.Destroy();
        foreach (var pair in Constants.STRUCTURE_MAP)
        {
            guis.Add(new StructureGUI(pair.Key));
        }
    }
    public static void FillRightSide(StructureInfo info)
    {
        if(infoGUI != null)
            infoGUI.Destroy();
        infoGUI = new StructureInfoGUI(info);
    }
    public static void SetStructure(EStructure structure)
    {
        Constants.STRUCTURE_PANEL.SetActive(false);
        Constants.COLONY_PANEL.SetActive(true);
        if (Selection.CurrentSelection.ModelObject is IColonizable c && c.ColonizableManager is IColonizableManager m && m.Colony is Colony colony)
            SetStructure(structure, c, colony);
    }
    private static void SetStructure(EStructure structure, IColonizable c, Colony colony)
    {
        colony.AddStructure(structure);
        ColonyDialogController.Reset(c);
        GoodsDialogController.Update(c);
    }
}
