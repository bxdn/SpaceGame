using Assets.Scripts;
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
        Constants.WATERF.SetActive(activated);
        Constants.METALSF.SetActive(activated);
        Constants.GASSESF.SetActive(activated);
        Constants.ENERGYF.SetActive(activated);

        Constants.NAMEL.SetActive(activated);
        Constants.TYPEL.SetActive(activated);
        Constants.OBL.SetActive(activated);
        Constants.COLL.SetActive(activated);
        Constants.SIZEL.SetActive(activated);
        Constants.ARABLEL.SetActive(activated);
        Constants.OTHERL.SetActive(activated);
        Constants.HAZARDL.SetActive(activated);
        Constants.WATERL.SetActive(activated);
        Constants.METALSL.SetActive(activated);
        Constants.GASSESL.SetActive(activated);
        Constants.ENERGYL.SetActive(activated);
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
            IColonizableManager colManager = colonizable.ColonizableManager;
            if(colManager is ColonizableManager manager)
            {
                IDictionary<EResource, int> resources = manager.Resources;
                Constants.COLF.GetComponent<Text>().text = manager.Owner == null ? "No" : "Yes";
                Constants.ARABLEF.GetComponent<Text>().text = manager.ArableLand.ToString();
                Constants.OTHERF.GetComponent<Text>().text = manager.OtherLand.ToString();
                Constants.HAZARDF.GetComponent<Text>().text = manager.HazardFrequency.ToString();
                Constants.WATERF.GetComponent<Text>().text = resources[EResource.Water].ToString();
                Constants.METALSF.GetComponent<Text>().text = resources[EResource.Metals].ToString();
                Constants.GASSESF.GetComponent<Text>().text = resources[EResource.Gasses].ToString();
                Constants.ENERGYF.GetComponent<Text>().text = resources[EResource.EnergySource].ToString();
            }
        }
    }

    public static void LayoutUI()
    {
        float col2X = Screen.width / 3f;
        float col3X = (Screen.width / 3f) * 2;
        float col2FieldX = col2X + 200;
        float col3FieldX = col3X + 200;
        Constants.SIZEL.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2X, 0);
        Constants.ARABLEL.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2X, -25);
        Constants.OTHERL.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2X, -50);
        Constants.HAZARDL.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2X, -75);
        Constants.SIZEF.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2FieldX, 0);
        Constants.ARABLEF.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2FieldX, -25);
        Constants.OTHERF.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2FieldX, -50);
        Constants.HAZARDF.GetComponent<RectTransform>().anchoredPosition = new Vector2(col2FieldX, -75);
        Constants.WATERL.GetComponent<RectTransform>().anchoredPosition = new Vector2(col3X, 0);
        Constants.METALSL.GetComponent<RectTransform>().anchoredPosition = new Vector2(col3X, -25);
        Constants.GASSESL.GetComponent<RectTransform>().anchoredPosition = new Vector2(col3X, -50);
        Constants.ENERGYL.GetComponent<RectTransform>().anchoredPosition = new Vector2(col3X, -75);
        Constants.WATERF.GetComponent<RectTransform>().anchoredPosition = new Vector2(col3FieldX, 0);
        Constants.METALSF.GetComponent<RectTransform>().anchoredPosition = new Vector2(col3FieldX, -25);
        Constants.GASSESF.GetComponent<RectTransform>().anchoredPosition = new Vector2(col3FieldX, -50);
        Constants.ENERGYF.GetComponent<RectTransform>().anchoredPosition = new Vector2(col3FieldX, -75);
    }
}
