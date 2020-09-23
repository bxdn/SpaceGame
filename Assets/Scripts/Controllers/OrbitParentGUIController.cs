using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrbitParentGUIController : MonoBehaviour, ISelectable
{
    private float clickTime = 0;
    public Transform bigTransform;
    public Text text;
    public IOrbitChild System { get; set; }
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
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0 && !CameraController.Locked)
        {
            Utils.Scale(bigTransform, scrollDelta);
            Transform textTransform = text.gameObject.transform;
            Utils.Scale(textTransform, scrollDelta);
        }
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject() || CameraController.Locked)
        {
            return;
        }
        mouseOn = true;
    }

    private void OnMouseExit()
    {
        if (EventSystem.current.IsPointerOverGameObject() || CameraController.Locked)
        {
            return;
        }
        mouseOn = false;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject() || CameraController.Locked)
        {
            return;
        }
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
        bool colonyButtonActivated = false;
        Utils.SetUIActivated(System is Planet);
        if (System is Planet planet)
        {
            Utils.SetUIActivated(true);
            Utils.FillUI(planet);
            if (planet is IColonizable c && c.ColonizableManager.Owner == Player.Domain)
               colonyButtonActivated = true;
        }
        else
        {
            Utils.SetUIActivated(false);
        }
        Constants.COLONY_BUTTON.SetActive(colonyButtonActivated);
    }

    public void Deselect()
    {
        selected = false;
    }
}

