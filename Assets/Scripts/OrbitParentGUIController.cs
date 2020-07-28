using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbitParentGUIController : MonoBehaviour
{
    private float clickTime = 0;
    public Transform bigTransform;
    public Text text;
    public IOrbitChild System { get; set; }
    private bool mouseOn = false;

    // Update is called once per frame
    void Update()
    {
        float alpha = text.color.a;
        if (alpha < 1 && mouseOn)
        {
            text.color = new Color(0, 0, 0, alpha + .05f);
        }
        else if (alpha > 0 && !mouseOn)
        {
            text.color = new Color(0, 0, 0, alpha - .05f);
        }
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0)
        {
            Vector3 scale = bigTransform.localScale;
            bigTransform.localScale = scrollDelta > 0 ? scale / 1.2f : scale * 1.2f;
            Transform textTransform = text.gameObject.transform;
            scale = textTransform.localScale;
            textTransform.localScale = scrollDelta > 0 ? scale / 1.2f : scale * 1.2f;
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
    }
}

