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
        foreach (var pair in Constants.FEATURE_MAP)
        {
            if(pair.Key is EStructure struc  && struc != EStructure.LogisticsStation)
                guis.Add(new StructureGUI(struc));
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
        Constants.COLONY_PANEL.SetActive(false);
        Constants.GOODS_PANEL.SetActive(false);
        CameraController.Locked = false;
        if (Selection.CurrentSelection is IColonizable c && c.ColonizableManager is IColonizableManager m)
            AddStructureController.Activate(structure, m);
    }
}
