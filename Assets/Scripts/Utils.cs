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

    internal static void FillUI(Orbiter orbiter)
    {
        foreach(KeyValuePair<EField, String> pair in orbiter.Fields)
        {
            Constants.FIELDS[pair.Key].GetComponent<Text>().text = pair.Value;
        }
    }
}
