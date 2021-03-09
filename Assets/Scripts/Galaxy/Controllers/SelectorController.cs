using System;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
    private static readonly float Select1BaseScaleDEFAULT = 5f;
    private static readonly float Select2BaseScaleDEFAULT = 4.25f;
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
        TryScroll();
        TrySelectOrDeselect(Constants.SELECTION1.GetComponent<SpriteRenderer>(), 
            Constants.SELECTION2.GetComponent<SpriteRenderer>());
    }
    private void TryScroll()
    {
        var scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0)
            Scroll(scrollDelta);
    }
    private void TrySelectOrDeselect(SpriteRenderer renderer1, SpriteRenderer renderer2)
    {
        if (selecting) 
            ContinueSelecting(renderer1, renderer2);
        else if (deselecting) 
            ContinueDeselecting(renderer1, renderer2);
    }
    private void ContinueSelecting(SpriteRenderer renderer1, SpriteRenderer renderer2)
    {
        selecting = false;
        Scale();
        BoostAlpha(renderer1, renderer2);
    }
    private void Scale()
    {
        ScaleUpIfExpanding(Constants.SELECTION1.transform, Select1BaseScale);
        ScaleUpIfExpanding(Constants.SELECTION2.transform, Select2BaseScale);
    }
    private void ContinueDeselecting(SpriteRenderer renderer1, SpriteRenderer renderer2)
    {
        deselecting = false;
        DeScale();
        ReduceAlpha(renderer1, renderer2);
    }
    private void ReduceAlpha(SpriteRenderer renderer1, SpriteRenderer renderer2)
    {
        ReduceAlphaIfDisappearing(renderer1, new Color(1, 1, 1, renderer1.color.a - ALPHA_TICK));
        ReduceAlphaIfDisappearing(renderer2, new Color(0, 0, 0, renderer2.color.a - ALPHA_TICK));
    }
    private void ReduceAlphaIfDisappearing(SpriteRenderer renderer, Color color)
    {
        if (renderer.color.a > 0)
            DecreaseAlpha(renderer, color);
    }
    private void DecreaseAlpha(SpriteRenderer renderer, Color color)
    {
        renderer.color = color;
        deselecting = true;
    }
    private void DeScale()
    {
        ScaleDownIfRetracting(Constants.SELECTION1.transform, Select1BaseScale - SCALE_DELTA);
        ScaleDownIfRetracting(Constants.SELECTION2.transform, Select2BaseScale - SCALE_DELTA);
    }
    private void ScaleDownIfRetracting(Transform transform, float minScale)
    {
        if (transform.localScale.x > minScale)
            ScaleDown(transform);
    }
    private void ScaleDown(Transform transform)
    {
        transform.localScale -= SCALE_TICK;
        deselecting = true;
    }
    private void BoostAlpha(SpriteRenderer renderer1, SpriteRenderer renderer2)
    {
        IncreaseAlphaIfAppearing(renderer1, new Color(1, 1, 1, renderer1.color.a + ALPHA_TICK));
        IncreaseAlphaIfAppearing(renderer2, new Color(0, 0, 0, renderer2.color.a + ALPHA_TICK));
    }
    private void IncreaseAlphaIfAppearing(SpriteRenderer renderer, Color color)
    {
        if (renderer.color.a < 1)
            IncreaseAlpha(renderer, color);
    }
    private void ScaleUpIfExpanding(Transform transform, float maxScale)
    {
        if (transform.localScale.x < maxScale)
            ScaleUp(transform);
    }
    private void ScaleUp(Transform transform)
    {
        transform.localScale += SCALE_TICK;
        selecting = true;
    }

    private void IncreaseAlpha(SpriteRenderer renderer, Color color)
    {
        renderer.color = color;
        selecting = true;
    }
    private void Scroll(float scrollDelta)
    {
        Scroll(Constants.SELECTION1.transform, Constants.SELECTION2.transform, scrollDelta);
        SCALE_DELTA = Utils.Scale(SCALE_DELTA, scrollDelta);
        SCALE_TICK = Utils.Scale(SCALE_TICK, scrollDelta);
    }
    private void Scroll(Transform select1Transform, Transform select2Transform, float scrollDelta)
    {
        Utils.Scale(select1Transform, scrollDelta);
        Select1BaseScale = Utils.Scale(Select1BaseScale, scrollDelta);
        Utils.Scale(select2Transform, scrollDelta);
        Select2BaseScale = Utils.Scale(Select2BaseScale, scrollDelta);
    }
    public static void Select(Vector2 location)
    {
        Select(location, Constants.SELECTION1.transform, Constants.SELECTION2.transform);
        deselecting = false;
        selecting = true;
    }
    private static void Select(Vector2 location, Transform select1Transform, Transform select2Transform)
    {
        select1Transform.localPosition = location;
        select2Transform.localPosition = location;
        if (!location.Equals(currentLocation))
            SelectNew(select1Transform, select2Transform, location);
    }
    private static void SelectNew(Transform select1Transform, Transform select2Transform, Vector2 location)
    {
        ResetScale(select1Transform, Select1BaseScale - SCALE_DELTA);
        ResetScale(select2Transform, Select2BaseScale - SCALE_DELTA);
        currentLocation = location;
    }
    private static void ResetScale(Transform transform, float startScale)
    {
        transform.localScale = new Vector3(startScale, startScale, 1);
    }

    public static void Deselect()
    {
        deselecting = true;
        selecting = false;
    }

    public static void Reset()
    {
        ResetBaseScales();
        RepositionGUIS(new Vector3(-10000, -10000, 0));
        Constants.SELECTION1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        ResetMembers();
    }
    private static void ResetMembers()
    {
        currentLocation = Vector2.zero;
        SCALE_TICK = new Vector3(.04f, .04f, 0);
        SCALE_DELTA = 0.5f;
    }
    private static void ResetBaseScales()
    {
        Select1BaseScale = Select1BaseScaleDEFAULT;
        Select2BaseScale = Select2BaseScaleDEFAULT;
    }
    private static void RepositionGUIS(Vector3 location)
    {
        Constants.SELECTION1.transform.localPosition = location;
        Constants.SELECTION2.transform.localPosition = location;
    }
}
