using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColonyGUIController : MonoBehaviour, ISelectable
{
    private float clickTime = 0;
    public Transform bigTransform;
    public Text text;
    public IColonizable System { get; set; }
    private bool mouseOn = false;
    private bool selected = false;

    // Update is called once per frame
    void Update()
    {
        float alpha = text.color.a;
        if (alpha < 1 && (mouseOn || selected))
        {
            text.color = new Color(0, 0, 0, alpha + .05f);
        }
        else if (alpha > 0 && (!mouseOn && !selected))
        {
            text.color = new Color(0, 0, 0, alpha - .05f);
        }
    }

    private void OnMouseEnter()
    {
        mouseOn = true;
    }

    private void OnMouseExit()
    {
        mouseOn = false;
    }

    private void OnMouseDown()
    {
        float newClickTime = Time.time;
        if(newClickTime - clickTime < 0.3f)
        {
            System.Parent.RenderSystem();
        }
        clickTime = newClickTime;
        Selection.Select(this);
    }

    public void Select()
    {
        selected = true;
        if(System is Orbiter orbiter)
        {
            Utils.SetUIActivated(true);
            Utils.FillUI(orbiter);
        }
    }

    public void Deselect()
    {
        selected = false;
    }
}

