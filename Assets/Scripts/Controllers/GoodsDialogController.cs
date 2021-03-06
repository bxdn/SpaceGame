using Assets.Scripts.GUI;
using Assets.Scripts.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoodsDialogController : EventTrigger
{
    private bool dragging;
    private Vector3 curPos;
    private static readonly IList<GUIScrollable> goodGuis = new List<GUIScrollable>();
    private static int goodScrollVal = 0;

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
            if (scrollDelta < 0 && goodScrollVal < goodGuis.Count - 13)
            {
                goodScrollVal++;
                MoveStructGUIs(true);
            }
            else if (scrollDelta > 0 && goodScrollVal > 0)
            {
                goodScrollVal--;
                MoveStructGUIs(false);
            }
        }
    }
    private static void MoveStructGUIs(bool ascending)
    {
        foreach (GUIScrollable gui in goodGuis)
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
    public static void Reset(Colony c)
    {
        goodScrollVal = 0;
        if (c != null)
            Fill(c);
    }
    public static void Update(Colony c)
    {
        Fill(c);
        for (int i = 0; i < goodScrollVal; i++)
            MoveStructGUIs(true);
    }
    private static void Fill(Colony colony)
    {
        foreach (GUIDestroyable gui in goodGuis)
            gui.Destroy();
        goodGuis.Clear();
        int population = colony.Population;
        Vector2 currentPosition = new Vector2(0, 0);
        foreach (var good in colony.Goods)
        {
            goodGuis.Add(new GoodServiceGUI(good.Key, good.Value, currentPosition));
            currentPosition = new Vector2(0, currentPosition.y - 25);
        }
        foreach (var service in colony.Services)
        {
            goodGuis.Add(new GoodServiceGUI(service.Key, service.Value, currentPosition));
            currentPosition = new Vector2(0, currentPosition.y - 25);
        }
        currentPosition = new Vector2(300, 0);
        foreach (var good in colony.Level.GoodsPerPopNeeds)
        {
            goodGuis.Add(new DemandGUI(good.Key, good.Value * population, currentPosition));
            currentPosition = new Vector2(300, currentPosition.y - 25);
        }
        foreach (var service in colony.Level.ServicesPerPopNeeds)
        {
            goodGuis.Add(new DemandGUI(service.Key, service.Value * population, currentPosition));
            currentPosition = new Vector2(300, currentPosition.y - 25);
        }
        currentPosition = new Vector2(450, 0);
        foreach (var good in colony.Level.GoodsPerPopWants)
        {
            goodGuis.Add(new DemandGUI(good.Key, good.Value * population, currentPosition));
            currentPosition = new Vector2(450, currentPosition.y - 25);
        }
        foreach (var service in colony.Level.ServicesPerPopWants)
        {
            goodGuis.Add(new DemandGUI(service.Key, service.Value * population, currentPosition));
            currentPosition = new Vector2(450, currentPosition.y - 25);
        }
        Constants.POP_VAL.text = population.ToString();
        Constants.INF_VAL.text = Mathf.Floor(colony.Influence / (Mathf.Pow(2, colony.Level.CurrentLevel) * 200)).ToString();
        Constants.LVL_VAL.text = colony.Level.CurrentLevel.ToString();
    }
}
