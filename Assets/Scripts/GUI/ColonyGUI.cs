using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ColonyGUI : GUIDestroyable
{
    private readonly GameObject big;
    private readonly GameObject medium;
    private readonly GameObject small;
    private GameObject[] lines;
    private GameObject text;
    private IColonizable colonizable;
    private readonly Vector3 position;
    public ColonyGUI(IColonizable colonizable) : base()
	{
        CameraController.Locked = true;
        Vector3 position = CameraController.Camera.ScreenToWorldPoint(new Vector2(Screen.width / 2, (Screen.height / 2) - 52.5f));
        this.position = new Vector3(position.x, position.y, 0);
        big = new GameObject("Colony-Big");
        medium = new GameObject("Colony-Medium");
        small = new GameObject("Colony-Small");
        this.colonizable = colonizable;
        AddBig();
        AddMedium();
        AddSmall();
        AddLines();
        String name = colonizable.Name;
        Text text = AddUI(name);
        ColonyGUIController controller = small.AddComponent<ColonyGUIController>();
        controller.text = text;
        controller.System = colonizable;
    }

    private void AddLines()
    {
        ColonizableManager colonizableManager = colonizable.ColonizableManager as ColonizableManager;
        int numLines = colonizableManager.ArableLand + colonizableManager.OtherLand;
        float incrementalAngle = 360f / numLines;
        lines = new GameObject[numLines];
        for (int i = 0; i < numLines; i++){
            float angle = incrementalAngle * i;
            float x = position.x + 25 * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = position.y + 25 * Mathf.Sin(Mathf.Deg2Rad * angle);
            GameObject line = new GameObject("Line");
            Transform lineTransform = line.transform;
            lineTransform.localPosition = new Vector2(x,y);
            lineTransform.localScale = new Vector3(1f, 25, 1);
            lineTransform.parent = Constants.GRID.transform;
            lineTransform.Rotate(new Vector3(0, 0, 90 + angle));
            SpriteRenderer lineRenderer = line.AddComponent<SpriteRenderer>();
            lineRenderer.sprite = Constants.square;
            lineRenderer.color = Color.white;
            lineRenderer.sortingOrder = 4;
            lines[i] = line;
        }
    }

    private void AddBig()
    {
        Transform bigTransform = big.transform;
        bigTransform.localPosition = position;
        bigTransform.localScale = new Vector3(75, 75, 1);
        bigTransform.parent = Constants.GRID.transform;
        SpriteRenderer bigRenderer = big.AddComponent<SpriteRenderer>();
        bigRenderer.sprite = Constants.circle;
        bigRenderer.color = Color.white;
        bigRenderer.sortingOrder = 2;
    }

    private void AddMedium()
    {
        Transform medTransform = medium.transform;
        medTransform.localPosition = position;
        medTransform.localScale = new Vector3(70, 70, 1);
        medTransform.parent = Constants.GRID.transform;
        SpriteRenderer medRenderer = medium.AddComponent<SpriteRenderer>();
        medRenderer.sprite = Constants.circle;
        medRenderer.color = Color.black;
        medRenderer.sortingOrder = 3;
    }

    private void AddSmall()
    {
        Transform smallTransform = small.transform;
        smallTransform.localPosition = position;
        smallTransform.localScale = new Vector3(40, 40, 1);
        smallTransform.parent = Constants.GRID.transform;
        SpriteRenderer smallRenderer = small.AddComponent<SpriteRenderer>();
        smallRenderer.sprite = Constants.circle;
        smallRenderer.color = Color.white;
        smallRenderer.sortingOrder = 4;
        small.AddComponent<CircleCollider2D>();
    }

    private Text AddUI(String name)
    {
        text = new GameObject("Text", typeof(RectTransform));
        Text textComponent = text.AddComponent<Text>();
        textComponent.text = name;
        textComponent.fontSize = 72;
        textComponent.font = Constants.ARIAL;
        textComponent.color = new Color(0, 0, 0, 0);
        textComponent.lineSpacing = 0;
        textComponent.alignment = TextAnchor.MiddleCenter;
        RectTransform transform = (RectTransform)text.transform;
        transform.localScale = new Vector3(.04f, .04f, 1);
        transform.localPosition = position;
        Vector2 newSize = new Vector2(LayoutUtility.GetPreferredWidth((RectTransform)transform),
           LayoutUtility.GetPreferredHeight((RectTransform)transform));
        transform.sizeDelta = newSize;
        transform.SetParent(Constants.CANVAS.transform);
        return textComponent;
    }

    public override void Destroy()
    {
        UnityEngine.Object.Destroy(big);
        UnityEngine.Object.Destroy(medium);
        UnityEngine.Object.Destroy(small);
        UnityEngine.Object.Destroy(text);
        foreach(GameObject line in lines)
        {
            UnityEngine.Object.Destroy(line);
        }
    }
}
