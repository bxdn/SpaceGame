using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;

public class OrbitParentGUI : GUIDestroyable
{
    private readonly GameObject big;
    private GameObject text;
    private IMiddleChild middleChild;
    public OrbitParentGUI(IMiddleChild orbitee) : base()
	{
        big = new GameObject("Star");
        middleChild = orbitee;
        AddBig();
        String name = orbitee.Name;
        Text text = AddUI(name);
        OrbitParentGUIController controller = big.AddComponent<OrbitParentGUIController>();
        controller.bigTransform = big.transform;
        controller.text = text;
        controller.System = orbitee;
        Selection.Select(controller);
    }

    private void AddBig()
    {
        Transform bigTransform = big.transform;
        bigTransform.localPosition = new Vector3(0, 0, 0);
        bigTransform.localScale = new Vector3(15, 15, 1);
        bigTransform.parent = Constants.GRID.transform;
        SpriteRenderer bigRenderer = big.AddComponent<SpriteRenderer>();
        bigRenderer.sprite = Constants.circle;
        bigRenderer.color = GetColor();
        bigRenderer.sortingOrder = 2;
        big.AddComponent<CircleCollider2D>();

        
    }

    private Color GetColor()
    {
        if(middleChild is IColonizable colonizable && colonizable.ColonizableManager.Owner == WorldGeneration.Galaxy.Player.Domain)
        {
            return Constants.colonizedColor;
        }
        else
        {
            return Color.white;
        }
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
        Vector2 newSize = new Vector2(LayoutUtility.GetPreferredWidth((RectTransform)transform),
           LayoutUtility.GetPreferredHeight((RectTransform)transform));
        transform.sizeDelta = newSize;
        transform.SetParent(Constants.CANVAS.transform);
        return textComponent;
    }

    public override void Destroy()
    {
        UnityEngine.Object.Destroy(big);
        UnityEngine.Object.Destroy(text);
    }
}
