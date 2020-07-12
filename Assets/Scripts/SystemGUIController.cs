using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemGUIController : MonoBehaviour
{
    private float clickTime = 0;
    public SolarSystem system;
    private void Update()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0)
        {
            Vector3 starScale = gameObject.transform.localScale;
            gameObject.transform.localScale = scrollDelta > 0 ? starScale / Constants.SCALE_TICK : starScale * Constants.SCALE_TICK;
        }
    }
    private void OnMouseEnter()
    {
        SelectorController.Select(new Vector2(transform.localPosition.x, transform.localPosition.y));
    }
    private void OnMouseExit()
    {
        SelectorController.Deselect();
    }

    private void OnMouseDown()
    {
        float newClickTime = Time.time;
        if (newClickTime - clickTime < 0.3f)
        {
            WorldGeneration.RenderSystem(system);
        }
        clickTime = newClickTime;
    }
}
