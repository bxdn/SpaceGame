using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColonyDialogController : EventTrigger
{
    private bool dragging;
    private Vector3 curPos;
    private static readonly IList<GUIScrollable> structGuis = new List<GUIScrollable>();
    private static int structScrollVal = 0;

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
            if (scrollDelta < 0 && structScrollVal < structGuis.Count - 20)
            {
                structScrollVal++;
                MoveStructGUIs(true);
            }
            else if (scrollDelta > 0 && structScrollVal > 0)
            {
                structScrollVal--;
                MoveStructGUIs(false);
            }
        }
    }
    private static void MoveStructGUIs(bool ascending)
    {
        foreach (GUIScrollable gui in structGuis)
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
    public static void Reset(Colony colony)
    {
        structScrollVal = 0;
        Fill(colony);
    }
    public static void Update(Colony c)
    {
        Fill(c);
        for (int i = 0; i < structScrollVal; i++)
            MoveStructGUIs(true);
    }
    private static void Fill(Colony colony)
    {
        if (colony == null)
            return;
        foreach (GUIDestroyable gui in structGuis)
            gui.Destroy();
        structGuis.Clear();
        Vector2 currentPosition = new Vector2(0, 0);
        Constants.COLONY_NAME_FIELD.text = colony.Name;
        foreach (var structure in colony.Structures)
        {
            structGuis.Add(new StructureCountGUI((StructureInfo)Constants.FEATURE_MAP[structure.Key], structure.Value.Count, currentPosition));
            currentPosition = new Vector2(0, currentPosition.y - 25);
        }
    }
}
