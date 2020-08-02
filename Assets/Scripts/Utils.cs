using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utils
{
	public static void Scale(Transform transform, float scrollDelta)
    {
        transform.localScale = scrollDelta > 0 ? transform.localScale / Constants.SCALE_TICK : transform.localScale * Constants.SCALE_TICK;
    }

    public static float Scale(float baseValue, float scrollDelta)
    {
       return scrollDelta > 0 ? baseValue / Constants.SCALE_TICK : baseValue * Constants.SCALE_TICK;
    }

    public static Vector3 Scale(Vector3 baseValue, float scrollDelta)
    {
        return scrollDelta > 0 ? baseValue / Constants.SCALE_TICK : baseValue * Constants.SCALE_TICK;
    }

    public static void SetUIActivated(bool activated)
    {
        Constants.NAMEF.SetActive(activated);
        Constants.TYPEF.SetActive(activated);
        Constants.OBF.SetActive(activated);
        Constants.COLF.SetActive(activated);
        Constants.SIZEF.SetActive(activated);
        Constants.ARABLEF.SetActive(activated);
        Constants.OTHERF.SetActive(activated);
        Constants.HAZARDF.SetActive(activated);

        Constants.NAMEL.SetActive(activated);
        Constants.TYPEL.SetActive(activated);
        Constants.OBL.SetActive(activated);
        Constants.COLL.SetActive(activated);
        Constants.SIZEL.SetActive(activated);
        Constants.ARABLEL.SetActive(activated);
        Constants.OTHERL.SetActive(activated);
        Constants.HAZARDL.SetActive(activated);
    }

    public static void FillUI(Orbiter orbiter)
    {
        foreach (GameObject field in Constants.FIELDS)
        {
            field.GetComponent<Text>().text = "N/A";
        }
        Constants.NAMEF.GetComponent<Text>().text = orbiter.Name;
        Constants.TYPEF.GetComponent<Text>().text = orbiter.Type;
        Constants.SIZEF.GetComponent<Text>().text = orbiter.Size.ToString();
        if (orbiter is IOrbitParent parent)
        {
            Constants.OBF.GetComponent<Text>().text = parent.Children.Length.ToString();
        }
        if (orbiter is IColonizable colonizable)
        {
            Constants.COLF.GetComponent<Text>().text = colonizable.ColonizableManager.Owner == null ? "No" : "Yes";
            Constants.ARABLEF.GetComponent<Text>().text = colonizable.ColonizableManager.ArableLand.ToString();
            Constants.OTHERF.GetComponent<Text>().text = colonizable.ColonizableManager.OtherLand.ToString();
            Constants.HAZARDF.GetComponent<Text>().text = colonizable.ColonizableManager.HazardFrequency.ToString();
        }
    }

    public static void LayoutUI()
    {
        float col2X = Screen.width / 3f;
        float col2FieldX = col2X + 200;
        Constants.SIZEL.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2X, 0);
        Constants.ARABLEL.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2X, -25);
        Constants.OTHERL.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2X, -50);
        Constants.HAZARDL.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2X, -75);
        Constants.SIZEF.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2FieldX, 0);
        Constants.ARABLEF.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2FieldX, -25);
        Constants.OTHERF.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2FieldX, -50);
        Constants.HAZARDF.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2FieldX, -75);
    }
}
