using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradePanelController : EventTrigger
{
    private static readonly IList<GUIScrollable> guis = new List<GUIScrollable>();
    private bool dragging;
    private Vector3 curPos;
    private static Colony colony;
    private static int scrollVal = 0;
    private static int idx = 0;
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
                MoveGUIs(true);
            }
            else if (scrollDelta > 0 && scrollVal > 0)
            {
                scrollVal--;
                MoveGUIs(false);
            }
        }
    }
    private static void MoveGUIs(bool ascending)
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
    public static void Fill(Colony col)
    {
        colony = col;
    }
    public static void AddRoute()
    {
        var field = Instantiate(Constants.INPUT_FIELD, Constants.TRADE_MASKING_PANEL.transform);
        (field.transform as RectTransform).anchoredPosition = new Vector2(125, -50 * idx);
        var iField = field.GetComponent<InputField>();
        var field2 = Instantiate(Constants.INPUT_FIELD, Constants.TRADE_MASKING_PANEL.transform);
        (field2.transform as RectTransform).anchoredPosition = new Vector2(350, -50 * idx);
        var iField2 = field2.GetComponent<InputField>();
        var fieldN = Instantiate(Constants.INPUT_FIELD_NUM, Constants.TRADE_MASKING_PANEL.transform);
        (fieldN.transform as RectTransform).anchoredPosition = new Vector2(475, -50 * idx);
        var iFieldN = fieldN.GetComponent<InputField>();
        var field3 = Instantiate(Constants.INPUT_FIELD, Constants.TRADE_MASKING_PANEL.transform);
        (field3.transform as RectTransform).anchoredPosition = new Vector2(600, -50 * idx);
        var iField3 = field3.GetComponent<InputField>();
        var fieldN2 = Instantiate(Constants.INPUT_FIELD_NUM, Constants.TRADE_MASKING_PANEL.transform);
        (fieldN2.transform as RectTransform).anchoredPosition = new Vector2(725, -50 * idx);
        var iFieldN2 = fieldN2.GetComponent<InputField>();
        var field4 = Instantiate(Constants.ADD_BUTTON, Constants.TRADE_MASKING_PANEL.transform);
        (field4.transform as RectTransform).anchoredPosition = new Vector2(800, -50 * idx++);
        field4.GetComponent<ConfirmRouteButtonController>().Init(iField, iField2, iField3, iFieldN, iFieldN2);
    }
}
