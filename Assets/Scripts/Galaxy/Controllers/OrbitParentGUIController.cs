using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrbitParentGUIController : MonoBehaviour
{
    private float clickTime = 0;
    public Transform bigTransform;
    public Text text;
    public IMiddleChild System { get; set; }

    public IModelObject ModelObject => System;

    private bool mouseOn = false;
    private bool selected = false;

    // Update is called once per frame
    void Update()
    {
        if (selected && Selection.CurrentSelection != ModelObject)
            Deselect();
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
            return;
        float newClickTime = Time.time;
        if(newClickTime - clickTime < 0.3f)
            System.Parent.RenderSystem();
        else
        {
            clickTime = newClickTime;
            Select();
        }
    }

    public void Select()
    {
        Selection.Select(ModelObject);
        selected = true;
        bool colonyButtonActivated = false;
        Utils.SetUIActivated(System is Planet);
        if (System is Planet planet)
        {
            Utils.SetUIActivated(true);
            Utils.FillUI(planet);
            if (planet is IColonizable)
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

