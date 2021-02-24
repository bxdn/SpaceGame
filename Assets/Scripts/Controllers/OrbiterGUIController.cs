using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrbiterGUIController : MonoBehaviour, ISelectable
{
    private float startTime;
    private static readonly float dur = .25f;
    private float zoomDelta = 5;
    private float clickTime = 0;
    private float bigMin = OrbiterGUI.bigMin;
    private float smallMin = OrbiterGUI.smallMin;

    public Transform bigTransform;
    public Transform smallTransform;
    public Text text;
    public IChild Orbiter { get; set; }
    public IModelObject ModelObject => Orbiter;
    private float solarDistance = -1;
    private bool expanding = false;
    private bool retracting = false;
    private bool selected = false;

    // Update is called once per frame
    void Update()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0 && !CameraController.Locked)
        {
            Utils.Scale(bigTransform, scrollDelta);
            Utils.Scale(smallTransform, scrollDelta);
            Utils.Scale(text.gameObject.transform, scrollDelta);
            bigMin = Utils.Scale(bigMin, scrollDelta);
            smallMin = Utils.Scale(smallMin, scrollDelta);
            zoomDelta = Utils.Scale(zoomDelta, scrollDelta);
        }
        if (expanding)
        {
            Expand();
        }
        else if (retracting)
        {
            Retract();
        }
        Rotate();
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject() || CameraController.Locked)
        {
            return;
        }
        if (!selected)
        {
            expanding = true;
            if (!retracting)
            {
                startTime = Time.time;
            }
            else
            {
                startTime = Time.time - (dur - (Time.time - startTime));
                retracting = false;
            }
        }
    }

    private void OnMouseExit()
    {
        if (EventSystem.current.IsPointerOverGameObject() || CameraController.Locked)
        {
            return;
        }
        if (!selected)
        {
            BeginRetraction();
        }
    }

    private void BeginRetraction()
    {
        retracting = true;
        if (!expanding)
        {
            startTime = Time.time;
        }
        else
        {
            startTime = Time.time - (dur - (Time.time - startTime));
            expanding = false;
        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject() || CameraController.Locked)
            return;
        float newClickTime = Time.time;
        if (newClickTime - clickTime < 0.3f && Orbiter is IParent orbitParent)
            orbitParent.RenderSystem();
        else
        {
            clickTime = newClickTime;
            Selection.Select(this);
        }
    }

    private void Expand()
    {
        float t = (Time.time - startTime) / dur;
        float bigStep = Mathf.SmoothStep(bigMin, bigMin + zoomDelta, t);
        bigTransform.localScale = new Vector3(bigStep, bigStep, 1);
        float smallStep = Mathf.SmoothStep(smallMin, smallMin + zoomDelta, t);
        smallTransform.localScale = new Vector3(smallStep, smallStep, 1);
        float alpha = text.color.a;
        if(alpha < 1)
        {
            text.color = new Color(1, 1, 1, alpha + .05f);
        }
        if (startTime + dur <= Time.time)
        {
            expanding = false;
        }
    }

    private void Retract()
    {
        retracting = false;
        float alpha = text.color.a;
        if (alpha > 0)
        {
            text.color = new Color(1, 1, 1, alpha - .05f);
            retracting = true;
        }
        if (startTime + dur > Time.time)
        {
            float t = (Time.time - startTime) / dur;
            float bigStep = Mathf.SmoothStep(bigMin + zoomDelta, bigMin, t);
            bigTransform.localScale = new Vector3(bigStep, bigStep, 1);
            float smallStep = Mathf.SmoothStep(smallMin + zoomDelta, smallMin, t);
            smallTransform.localScale = new Vector3(smallStep, smallStep, 1);
            retracting = true;
        }
    }

    private void Rotate()
    {
        float x = bigTransform.localPosition.x;
        float y = bigTransform.localPosition.y;
        if (solarDistance == -1)
        {
            solarDistance = Mathf.Sqrt(x * x + y * y);
        }
        float rotatedX = Mathf.Cos(1 / (50 * solarDistance)) * x - Mathf.Sin(1 / (50 * solarDistance)) * y;

        float rotatedY = Mathf.Sin(1 / (50 * solarDistance)) * x + Mathf.Cos(1 / (50 * solarDistance)) * y;

        bigTransform.localPosition = new Vector3(rotatedX, rotatedY, 1);
        smallTransform.localPosition = new Vector3(rotatedX, rotatedY, 0);
        text.transform.localPosition = new Vector3(rotatedX, rotatedY, 0);
    }

    public void Select()
    {
        bool colonyButtonActivated = false;
        selected = true;
        if (Orbiter is Orbiter orbiter)
        {
            Utils.SetUIActivated(true);
            Utils.FillUI(orbiter);
            if (orbiter is IColonizable c && c.ColonizableManager.Owner == WorldGeneration.Galaxy.Player.Domain)
                colonyButtonActivated = true;
        }
        Constants.COLONY_BUTTON.SetActive(colonyButtonActivated);
    }

    public void Deselect()
    {
        selected = false;
        Constants.COLONY_BUTTON.SetActive(false);
        BeginRetraction();
    }
}
