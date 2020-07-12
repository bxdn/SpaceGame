using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetGUIController : MonoBehaviour
{
    private float startTime;
    private static readonly float dur = .25f;
    private float zoomDelta = 5;
    private float bigMin = PlanetGUI.bigMin;
    private float smallMin = PlanetGUI.smallMin;

    public Transform bigTransform;
    public Transform smallTransform;
    public Text text;
    private float solarDistance = -1;
    private bool expanding = false;
    private bool retracting = false;

    // Update is called once per frame
    void Update()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0)
        {
            Vector3 scale = bigTransform.localScale;
            bigTransform.localScale = scrollDelta > 0 ? scale / 1.2f : scale * 1.2f;
            scale = smallTransform.localScale;
            smallTransform.localScale = scrollDelta > 0 ? scale / 1.2f : scale * 1.2f;
            Transform textTransform = text.gameObject.transform;
            scale = textTransform.localScale;
            textTransform.localScale = scrollDelta > 0 ? scale / 1.2f : scale * 1.2f;
            bigMin = scrollDelta > 0 ? bigMin / 1.2f : bigMin * 1.2f;
            smallMin = scrollDelta > 0 ? smallMin / 1.2f : smallMin * 1.2f;
            zoomDelta = scrollDelta > 0 ? zoomDelta / 1.2f : zoomDelta * 1.2f;
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

    private void OnMouseExit()
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
}
