using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static readonly GameObject GRID = GameObject.Find("Grid");
    public static readonly GameObject CAMERA = GameObject.Find("Camera");
    public static readonly Sprite circle = Resources.Load<Sprite>("Circle");
    public static readonly Texture2D pointer = Resources.Load<Texture2D>("pointer");
    public static readonly GameObject SELECTION1 = GameObject.Find("Selection1");
    public static readonly GameObject SELECTION2 = GameObject.Find("Selection2");
    public static readonly GameObject CANVAS = GameObject.Find("Canvas");
    public static readonly Font ARIAL = Resources.GetBuiltinResource<Font>("Arial.ttf");
    public static readonly String FMT = "0000";
    public static readonly Material MATERIAL = Resources.Load<Material>("Material");
    public static readonly float SCALE_TICK = 1.2f;
    public static readonly Color colonizedColor = new Color(.4f, .8f, 1);

    public static readonly GameObject NAMEF = GameObject.Find("Name Field");
    public static readonly GameObject TYPEF = GameObject.Find("Type Field");
    public static readonly GameObject OBF = GameObject.Find("Orbiting Bodies Field");
    public static readonly GameObject COLF = GameObject.Find("Colonized Field");
    public static readonly GameObject SIZEF = GameObject.Find("Size Field");
    public static readonly GameObject ARABLEF = GameObject.Find("Arable Land Field");
    public static readonly GameObject OTHERF = GameObject.Find("Other Usable Land Field");
    public static readonly GameObject HAZARDF = GameObject.Find("Hazard Frequency Field");
    public static readonly GameObject WATERF = GameObject.Find("Water Field");
    public static readonly GameObject METALSF = GameObject.Find("Metals Field");
    public static readonly GameObject GASSESF = GameObject.Find("Gasses Field");
    public static readonly GameObject ENERGYF = GameObject.Find("Energy Sources Field");

    public static readonly GameObject NAMEL = GameObject.Find("Name");
    public static readonly GameObject TYPEL = GameObject.Find("Type");
    public static readonly GameObject OBL = GameObject.Find("Orbiting Bodies");
    public static readonly GameObject COLL = GameObject.Find("Colonized");
    public static readonly GameObject SIZEL = GameObject.Find("Size");
    public static readonly GameObject ARABLEL = GameObject.Find("Arable Land");
    public static readonly GameObject OTHERL = GameObject.Find("Other Usable Land");
    public static readonly GameObject HAZARDL = GameObject.Find("Hazard Frequency");
    public static readonly GameObject WATERL = GameObject.Find("Water");
    public static readonly GameObject METALSL = GameObject.Find("Metals");
    public static readonly GameObject GASSESL = GameObject.Find("Gasses");
    public static readonly GameObject ENERGYL = GameObject.Find("Energy Sources");

    public static readonly IList<GameObject> FIELDS = new List<GameObject>()
    {
        NAMEF ,
        TYPEF ,
        OBF ,
        COLF ,
        SIZEF ,
        ARABLEF ,
        OTHERF ,
        HAZARDF,
        WATERF,
        METALSF,
        GASSESF,
        ENERGYF
    };

    static Constants()
    {
        NAMEF.SetActive(false);
        TYPEF.SetActive(false);
        OBF.SetActive(false);
        COLF.SetActive(false);
        SIZEF.SetActive(false);
        ARABLEF.SetActive(false);
        OTHERF.SetActive(false);
        HAZARDF.SetActive(false);
        WATERF.SetActive(false);
        METALSF.SetActive(false);
        GASSESF.SetActive(false);
        ENERGYF.SetActive(false);

        NAMEL.SetActive(false);
        TYPEL.SetActive(false);
        OBL.SetActive(false);
        COLL.SetActive(false);
        SIZEL.SetActive(false);
        ARABLEL.SetActive(false);
        OTHERL.SetActive(false);
        HAZARDL.SetActive(false);
        WATERL.SetActive(false);
        METALSL.SetActive(false);
        GASSESL.SetActive(false);
        ENERGYL.SetActive(false);
        Utils.LayoutUI();
    }
}
