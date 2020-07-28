using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbiterGUI : GUIDestroyable
{
    private static readonly GameObject GRID = GameObject.Find("Grid");
    private static readonly Sprite circle = Resources.Load<Sprite>("Circle");
    private static readonly GameObject CANVAS = GameObject.Find("Canvas");
    private static readonly Font ARIAL = Resources.GetBuiltinResource<Font>("Arial.ttf");

    private readonly GameObject big;
    private readonly GameObject small;
    private GameObject text;

    public static readonly float bigMin = 7;
    public static readonly float smallMin = 6;

    public OrbiterGUI(Orbiter body) : base()
	{
        float initX = body.distance * Mathf.Cos(body.initialAngle);
        float initY = body.distance * Mathf.Sin(body.initialAngle);
        Vector2 loc = new Vector2(initX, initY);

        big = new GameObject("BigCircle");
        AddBig(loc);
        small = new GameObject("SmallCircle");
        AddSmall(loc);
        Text text = AddUI(loc, body.Name);
        OrbiterGUIController controller = big.AddComponent<OrbiterGUIController>();
        controller.bigTransform = big.transform;
        controller.smallTransform = small.transform;
        controller.text = text;
        controller.Orbiter = body;
    }

    private void AddBig(Vector2 loc)
    {
        Transform bigTransform = big.transform;
        bigTransform.localPosition = new Vector3(loc.x, loc.y, 1);
        bigTransform.localScale = new Vector3(bigMin, bigMin, 1);
        bigTransform.parent = GRID.transform;
        SpriteRenderer bigRenderer = big.AddComponent<SpriteRenderer>();
        bigRenderer.sprite = circle;
        bigRenderer.color = Color.white;
        big.AddComponent<CircleCollider2D>();
    }

    private void AddSmall(Vector2 loc)
    {
        Transform smallTransform = small.transform;
        smallTransform.localPosition = new Vector3(loc.x, loc.y, 0);
        smallTransform.localScale = new Vector3(smallMin, smallMin, 1);
        smallTransform.parent = GRID.transform;
        SpriteRenderer smallRenderer = small.AddComponent<SpriteRenderer>();
        smallRenderer.sprite = circle;
        smallRenderer.color = Color.black;
        smallRenderer.sortingOrder = 1;
    }

    private Text AddUI(Vector2 loc, String name)
    {
        text = new GameObject("Text", typeof(RectTransform));
        Text textComponent = text.AddComponent<Text>();
        textComponent.text = name;
        textComponent.fontSize = 72;
        textComponent.font = ARIAL;
        textComponent.color = new Color(1, 1, 1, 0);
        textComponent.lineSpacing = 0;
        textComponent.alignment = TextAnchor.MiddleCenter;
        RectTransform transform = (RectTransform)text.transform;
        transform.localScale = new Vector3(.035f, .035f, 1);
        transform.localPosition = new Vector3(loc.x, loc.y, 0);
        Vector2 newSize = new Vector2(LayoutUtility.GetPreferredWidth((RectTransform)transform),
           LayoutUtility.GetPreferredHeight((RectTransform)transform));
        transform.sizeDelta = newSize;
        transform.SetParent(CANVAS.transform);
        return textComponent;
    }

    public override void Destroy()
    {
        UnityEngine.Object.Destroy(big);
        UnityEngine.Object.Destroy(small);
        UnityEngine.Object.Destroy(text);
    }
}
