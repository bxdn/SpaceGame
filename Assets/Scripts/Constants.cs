using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using UnityEngine;
using UnityEngine.UI;

public static class Constants
{
    public static readonly GameObject GRID = GameObject.Find("Grid");
    public static readonly GameObject CAMERA = GameObject.Find("Camera");
    public static readonly Sprite circle = Resources.Load<Sprite>("Circle");
    public static readonly Sprite square = Resources.Load<Sprite>("Square");
    public static readonly Texture2D pointer = Resources.Load<Texture2D>("pointer");
    public static readonly GameObject SELECTION1 = GameObject.Find("Selection1");
    public static readonly GameObject SELECTION2 = GameObject.Find("Selection2");
    public static readonly GameObject CANVAS = GameObject.Find("Canvas");
    public static readonly Font ARIAL = Resources.GetBuiltinResource<Font>("Arial.ttf");
    public static readonly String FMT = "0000";
    public static readonly Material MATERIAL = Resources.Load<Material>("Material");
    public static readonly float SCALE_TICK = 1.2f;
    public static readonly Color colonizedColor = new Color(.4f, .8f, 1);

    public static readonly GameObject BLACK_SQUARE = Resources.Load<GameObject>("BlackSquare");
    public static readonly GameObject WHITE_SQUARE = Resources.Load<GameObject>("WhiteSquare");
    public static readonly GameObject LETTER = Resources.Load<GameObject>("Letter");

    public static readonly GameObject TOP_INFO = GameObject.Find("Top Info");
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

    public static readonly GameObject COLONY_PANEL = GameObject.Find("Panel");
    public static readonly GameObject STRUCTURE_PANEL = GameObject.Find("Structure Panel");
    public static readonly GameObject GOODS_PANEL = GameObject.Find("Goods Panel");
    public static readonly GameObject MASKING_PANEL = GameObject.Find("MaskingPanel");
    public static readonly GameObject GOOD_MASKING_PANEL = GameObject.Find("MaskingPanel2");
    public static readonly GameObject STRUCTURE_MASKING_PANEL = GameObject.Find("Structure Masking Panel");
    public static readonly GameObject COLONY_BUTTON = GameObject.Find("Colony Button");
    public static readonly GameObject COLONIZE_BUTTON = GameObject.Find("Colonize Button");
    public static readonly GameObject MENUS_BUTTON = GameObject.Find("Menus");
    public static readonly Text POP_VAL = GameObject.Find("PopVal").GetComponent<Text>();
    public static readonly Text INF_VAL = GameObject.Find("InfluenceVal").GetComponent<Text>();
    public static readonly Text LVL_VAL = GameObject.Find("LevelVal").GetComponent<Text>();
    public static readonly Text SHIPS_VAL = GameObject.Find("RocketField").GetComponent<Text>();

    public static readonly IDictionary<EResource, String> RESOURCE_MAP;
    public static readonly IDictionary<EGood, String> GOOD_MAP;
    public static readonly IDictionary<EService, String> SERVICE_MAP;
    public static readonly IDictionary<Enum, IFeatureInfo> FEATURE_MAP;

    public static readonly IList<GameObject> FIELDS = ImmutableList.Create(new GameObject[] 
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
    });
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

        COLONY_PANEL.SetActive(false);
        STRUCTURE_PANEL.SetActive(false);
        GOODS_PANEL.SetActive(false);
        COLONY_BUTTON.SetActive(false);
        COLONIZE_BUTTON.SetActive(false);
        MENUS_BUTTON.SetActive(false);
        Utils.LayoutUI();

        var featureBuilder = ImmutableDictionary.CreateBuilder<Enum, IFeatureInfo>();
        featureBuilder.Add(EStructure.Housing, StructureRegistry.Housing);
        featureBuilder.Add(EStructure.EnergyPlant, StructureRegistry.EnergyPlant);
        featureBuilder.Add(EStructure.Farm, StructureRegistry.Farm);
        featureBuilder.Add(EStructure.ElectrolysisPlant, StructureRegistry.ElectrolysisPlant);
        featureBuilder.Add(EStructure.IronMine, StructureRegistry.IronMine);
        featureBuilder.Add(EStructure.SteelSmelter, StructureRegistry.SteelSmelter);
        featureBuilder.Add(EStructure.WaterPlant, StructureRegistry.WaterCollectionPlant);
        featureBuilder.Add(EStructure.MylarPlant, StructureRegistry.MylarPlant);
        featureBuilder.Add(EStructure.CarbonGatherer, StructureRegistry.CarbonCollector);
        featureBuilder.Add(EStructure.AlcoholMaker, StructureRegistry.AlcoholMaker);
        featureBuilder.Add(EStructure.DoctorsOffices, StructureRegistry.DoctorsOffices);
        featureBuilder.Add(EStructure.ChipMaker, StructureRegistry.ChipMaker);
        featureBuilder.Add(EStructure.SiliconGatherer, StructureRegistry.SiliconCollector);
        featureBuilder.Add(EStructure.School, StructureRegistry.School);
        featureBuilder.Add(EStructure.WindowMaker, StructureRegistry.WindowMaker);
        featureBuilder.Add(EStructure.GlassBlower, StructureRegistry.GlassBlower);
        featureBuilder.Add(EStructure.CopperMine, StructureRegistry.CopperMine);
        featureBuilder.Add(EResource.Water, ResourceRegistry.Water);
        featureBuilder.Add(EResource.Iron, ResourceRegistry.Iron);
        featureBuilder.Add(EResource.Land, ResourceRegistry.Land);
        featureBuilder.Add(EResource.Silicon, ResourceRegistry.Silicon);
        featureBuilder.Add(EResource.Copper, ResourceRegistry.Copper);
        FEATURE_MAP = featureBuilder.ToImmutable();

        var goodBuilder = ImmutableDictionary.CreateBuilder<EGood, String>();
        goodBuilder.Add(EGood.Alcohol, "Alcohol");
        goodBuilder.Add(EGood.Clothes, "Clothes");
        goodBuilder.Add(EGood.Electronics, "Electronics");
        goodBuilder.Add(EGood.Energy, "Energy");
        goodBuilder.Add(EGood.EntertainmentItems, "EntertainmentItems");
        goodBuilder.Add(EGood.Fabric, "Fabric");
        goodBuilder.Add(EGood.Food, "Food");
        goodBuilder.Add(EGood.Hydrogen, "Hydrogen");
        goodBuilder.Add(EGood.Machinery, "Machinery");
        goodBuilder.Add(EGood.Iron, "Iron");
        goodBuilder.Add(EGood.Robotics, "Robotics");
        goodBuilder.Add(EGood.Steel, "Steel");
        goodBuilder.Add(EGood.Tools, "Tools");
        goodBuilder.Add(EGood.Water, "Water");
        goodBuilder.Add(EGood.Oxygen, "Oxygen");
        goodBuilder.Add(EGood.Mylar, "Mylar");
        goodBuilder.Add(EGood.Carbon, "Carbon");
        goodBuilder.Add(EGood.Chips, "Chips");
        goodBuilder.Add(EGood.Silicon, "Silicon");
        goodBuilder.Add(EGood.Glass, "Glass");
        goodBuilder.Add(EGood.Windows, "Windows");
        goodBuilder.Add(EGood.Copper, "Copper");
        GOOD_MAP = goodBuilder.ToImmutable();

        var serviceBuilder = ImmutableDictionary.CreateBuilder<EService, String>();
        serviceBuilder.Add(EService.Education, "Education");
        serviceBuilder.Add(EService.Entertainment, "Entertainment");
        serviceBuilder.Add(EService.FoodService, "FoodService");
        serviceBuilder.Add(EService.Healthcare, "Healthcare");
        serviceBuilder.Add(EService.Housing, "Housing");
        serviceBuilder.Add(EService.Luxury, "Luxury");
        serviceBuilder.Add(EService.Safety, "Safety");
        SERVICE_MAP = serviceBuilder.ToImmutable();
    }
}
