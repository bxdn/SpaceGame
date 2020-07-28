using System;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
    private static readonly float Select1BaseScaleDEFAULT = 3.25f;
    private static readonly float Select2BaseScaleDEFAULT = 2.75f;
    private static float Select1BaseScale = Select1BaseScaleDEFAULT;
    private static float Select2BaseScale = Select2BaseScaleDEFAULT;

    private static Vector3 SCALE_TICK = new Vector3(.16f, .16f, 0);
    private static readonly float ALPHA_TICK = .15f;
    private static float SCALE_DELTA = 2.5f;

    private static bool selecting = false;
    private static bool deselecting = false;
    private static Vector2 currentLocation = Vector2.zero;
    private void Update()
    {
        Transform select1Transform = Constants.SELECTION1.transform;
        Transform select2Transform = Constants.SELECTION2.transform;
        Vector3 select1Scale = select1Transform.localScale;
        Vector3 select2Scale = select2Transform.localScale;
        SpriteRenderer renderer1 = Constants.SELECTION1.GetComponent<SpriteRenderer>();
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0)
        {
            Utils.Scale(select1Transform, scrollDelta);
            Select1BaseScale = Utils.Scale(Select1BaseScale, scrollDelta);
            Utils.Scale(select2Transform, scrollDelta);
            Select2BaseScale = Utils.Scale(Select2BaseScale, scrollDelta);
            SCALE_DELTA = Utils.Scale(SCALE_DELTA, scrollDelta);
            SCALE_TICK = Utils.Scale(SCALE_TICK, scrollDelta);
        }
        if (selecting)
        {
            selecting = false;
            if (select1Transform.localScale.x < Select1BaseScale)
            {
                select1Transform.localScale = select1Scale + SCALE_TICK;
                selecting = true;
            }
            if (select2Transform.localScale.x < Select2BaseScale)
            {
                select2Transform.localScale = select2Scale + SCALE_TICK;
                selecting = true;
            }
            if (renderer1.color.a < 1)
            {
                renderer1.color = new Color(1, 1, 1, renderer1.color.a + ALPHA_TICK);
                selecting = true;
            }
        }
        else if (deselecting)
        {
            deselecting = false;
            if (select1Transform.localScale.x > SelectorController.Select1BaseScale - SCALE_DELTA)
            {
                select1Transform.localScale = select1Scale - SCALE_TICK;
                deselecting = true;
            }
            if (select2Transform.localScale.x > SelectorController.Select2BaseScale - SCALE_DELTA)
            {
                select2Transform.localScale = select2Scale - SCALE_TICK;
                deselecting = true;
            }
            if (renderer1.color.a > 0)
            {
                renderer1.color = new Color(1, 1, 1, renderer1.color.a - ALPHA_TICK);
                deselecting = true;
            }
        }
    }

    public static void Select(Vector2 location)
    {
        Transform select1Transform = Constants.SELECTION1.transform;
        Transform select2Transform = Constants.SELECTION2.transform;
        select1Transform.localPosition = location;
        select2Transform.localPosition = location;
        if (!location.Equals(currentLocation))
        {
            float select1StartScale = SelectorController.Select1BaseScale - SCALE_DELTA;
            select1Transform.localScale = new Vector3(select1StartScale, select1StartScale, 1);
            Constants.SELECTION1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            float select2StartScale = SelectorController.Select2BaseScale - SCALE_DELTA;
            select2Transform.localScale = new Vector3(select2StartScale, select2StartScale, 1);
            currentLocation = location;

        }
        deselecting = false;
        selecting = true;
    }

    public static void Deselect()
    {
        deselecting = true;
        selecting = false;
    }

    public static void Reset()
    {
        Select1BaseScale = Select1BaseScaleDEFAULT;
        Select2BaseScale = Select2BaseScaleDEFAULT;
        Vector3 location = new Vector3(-10000, -10000, 0);
        Constants.SELECTION1.transform.localPosition = location;
        Constants.SELECTION2.transform.localPosition = location;
        Constants.SELECTION1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        currentLocation = Vector2.zero;
        SCALE_TICK = new Vector3(.04f, .04f, 0);
        SCALE_DELTA = 0.5f;
    }
}
