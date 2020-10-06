using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StructureLabelController : EventTrigger
{
    public LandUnit Unit { get; set; }

    public override void OnPointerDown(PointerEventData eventData)
    {
        LaunchStructureDialog();
    }
    private void LaunchStructureDialog()
    {
        Constants.STRUCTURE_PANEL.SetActive(true);
        Constants.COLONY_PANEL.SetActive(false);
        StructurePanelController.Fill(Unit);
    }
}
