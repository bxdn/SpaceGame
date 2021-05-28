using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using Assets.Scripts.Trade.Controllers;
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
    private static bool newRouteUnderConstruction = false;
    private Vector3 curPos;
    private static int idx = 0;
    private static InputField[] currentInputFields = new InputField[5];
    private static ConfirmRouteButtonController confirmController;
    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Vector3 pos = UnityEngine.Input.mousePosition - curPos;
            curPos = UnityEngine.Input.mousePosition;
            transform.Translate(pos);
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            dragging = true;
            curPos = UnityEngine.Input.mousePosition;
        }
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (UnityEngine.Input.GetMouseButtonUp(0))
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
        AddDeleteButton(route);
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
        AddDeleteButton(route);
    }
    private static void AddDeleteButton(TradeRoute route)
    {
        var trashButton = Instantiate(Constants.TRASH, Constants.TRADE_MASKING_PANEL.transform);
        (trashButton.transform as RectTransform).anchoredPosition = new Vector2(800, -50 * idx++);
        trashButton.GetComponent<DeleteRouteButtonController>().Init(route);
        guis.Add(trashButton);
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
        if (newRouteUnderConstruction)
            return;
        newRouteUnderConstruction = true;
        AddRoute();
        var addButton = Instantiate(Constants.ADD_BUTTON, Constants.TRADE_MASKING_PANEL.transform);
        guis.Add(addButton);
        (addButton.transform as RectTransform).anchoredPosition = new Vector2(800, -50 * idx++);
        var confirmController = addButton.GetComponent<ConfirmRouteButtonController>();
        confirmController.Init(currentInputFields[0], currentInputFields[1], 
            currentInputFields[3], currentInputFields[2], currentInputFields[4]);
        TradePanelController.confirmController = confirmController;

        (Constants.STEEL_COST_F.transform as RectTransform).anchoredPosition = new Vector2(150, -50 * (idx+1));
        (Constants.STEEL_COST_L.transform as RectTransform).anchoredPosition = new Vector2(10, -50 * (idx+1));
        (Constants.HYDROGEN_COST_F.transform as RectTransform).anchoredPosition = new Vector2(150, -50 * (idx + 2));
        (Constants.HYDROGEN_COST_L.transform as RectTransform).anchoredPosition = new Vector2(10, -50 * (idx + 2));

        Constants.STEEL_COST_F.SetActive(true);
        Constants.STEEL_COST_L.SetActive(true);
        Constants.HYDROGEN_COST_F.SetActive(true);
        Constants.HYDROGEN_COST_L.SetActive(true);
    }
    private static void DisableFields()
    {
        foreach (var field in currentInputFields)
            field.interactable = false;
    }

    public static void NotifyChanged()
    {
        var cost = confirmController.GetCost();
        Constants.STEEL_COST_F.GetComponent<Text>().text = cost == -1 ? "N/A" : cost.ToString();
        Constants.HYDROGEN_COST_F.GetComponent<Text>().text = cost == -1 ? "N/A" : Mathf.Sqrt(cost).ToString();
    }

    public static void Reset(Colony colony)
    {
        newRouteUnderConstruction = false;

        Constants.STEEL_COST_F.SetActive(false);
        Constants.STEEL_COST_L.SetActive(false);
        Constants.HYDROGEN_COST_F.SetActive(false);
        Constants.HYDROGEN_COST_L.SetActive(false);

        foreach (var gui in guis)
            Destroy(gui);
        guis.Clear();
        idx = 0;
        if(colony != null)
            Fill(colony);
    }
}
