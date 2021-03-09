using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using Assets.Scripts.Trade.Model;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradePanelController : EventTrigger
{
    private static readonly IList<GameObject> guis = new List<GameObject>();
    private bool dragging;
    private Vector3 curPos;
    private static int idx = 0;
    private static InputField[] currentInputFields = new InputField[5];
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
    private static void Fill(Colony col)
    {
        var tradeManager = col.TradeManager;
        foreach (var route in tradeManager.OutGoingRoutes)
            AddOutgoingRoute(route);
        foreach (var route in tradeManager.IncomingRoutes)
            AddIncomingRoute(route);
    }
    private static void AddOutgoingRoute(TradeRoute route)
    {
        AddRoute();
        currentInputFields[0].text = route.Receiver.Name;
        currentInputFields[1].text = Constants.GOOD_MAP[route.SentGood];
        currentInputFields[2].text = route.SentAmount.ToString();
        currentInputFields[3].text = Constants.GOOD_MAP[route.ReceivedGood];
        currentInputFields[4].text = route.ReceivedAmount.ToString();
        DisableFields();
        idx++;
    }
    private static void AddIncomingRoute(TradeRoute route)
    {
        AddRoute();
        currentInputFields[0].text = route.Originator.Name;
        currentInputFields[3].text = Constants.GOOD_MAP[route.SentGood];
        currentInputFields[4].text = route.SentAmount.ToString();
        currentInputFields[1].text = Constants.GOOD_MAP[route.ReceivedGood];
        currentInputFields[2].text = route.ReceivedAmount.ToString();
        DisableFields();
        idx++;
    }
    private static void AddRoute()
    {
        InstantiateField(Constants.INPUT_FIELD, 125, 0);
        InstantiateField(Constants.INPUT_FIELD, 350, 1);
        InstantiateField(Constants.INPUT_FIELD_NUM, 475, 2);
        InstantiateField(Constants.INPUT_FIELD, 600, 3);
        InstantiateField(Constants.INPUT_FIELD_NUM, 725, 4);
    }
    private static void InstantiateField(GameObject obj, int xVal, int inptFieldIdx)
    {
        var field = Instantiate(obj, Constants.TRADE_MASKING_PANEL.transform);
        (field.transform as RectTransform).anchoredPosition = new Vector2(xVal, -50 * idx);
        guis.Add(field);
        currentInputFields[inptFieldIdx] = field.GetComponent<InputField>();
    }
    public static void AddNewRoute()
    {
        AddRoute();
        var field4 = Instantiate(Constants.ADD_BUTTON, Constants.TRADE_MASKING_PANEL.transform);
        guis.Add(field4);
        (field4.transform as RectTransform).anchoredPosition = new Vector2(800, -50 * idx++);
        field4.GetComponent<ConfirmRouteButtonController>().Init(currentInputFields[0], currentInputFields[1], 
            currentInputFields[3], currentInputFields[2], currentInputFields[4]);
    }
    private static void DisableFields()
    {
        foreach (var field in currentInputFields)
            field.interactable = false;
    }

    internal static void Reset(Colony colony)
    {
        foreach(var gui in guis)
            Destroy(gui);
        guis.Clear();
        idx = 0;
        if(colony != null)
            Fill(colony);
    }
}
