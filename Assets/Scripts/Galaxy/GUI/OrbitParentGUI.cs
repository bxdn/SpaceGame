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
        AddController(big.AddComponent<OrbitParentGUIController>(), AddUI(orbitee.Name), orbitee);
        Selection.Select(orbitee);
    }
    private void AddController(OrbitParentGUIController controller, Text text, IMiddleChild orbitee)
    {
        controller.bigTransform = big.transform;
        controller.text = text;
        controller.System = orbitee;
        controller.Select();
    }
    private void AddBig()
    {
        AddBigTransform(big.transform);
        AddBigRenderer(big.AddComponent<SpriteRenderer>());
    }
    private void AddBigTransform(Transform bigTransform)
    {
        bigTransform.localPosition = new Vector3(0, 0, 0);
        bigTransform.localScale = new Vector3(15, 15, 1);
        bigTransform.parent = Constants.GRID.transform;
    }
    private void AddBigRenderer(SpriteRenderer bigRenderer)
    {
        bigRenderer.sprite = Constants.circle;
        bigRenderer.color = 
            middleChild is IColonizable colonizable
            && colonizable.ColonizableManager.Owner == WorldGeneration.Galaxy.Player.Domain
            ? Constants.colonizedColor 
            : Color.white;
        bigRenderer.sortingOrder = 2;
        big.AddComponent<CircleCollider2D>();
    }

    private Text AddUI(String name)
    {
        text = new GameObject("Text", typeof(RectTransform));
        var textComponent = AddText(text.AddComponent<Text>(), name);
        AddTransform((RectTransform)text.transform);
        return textComponent;
    }
    private Text AddText(Text textComponent, String name)
    {
        textComponent.text = name;
        textComponent.fontSize = 72;
        textComponent.font = Constants.ARIAL;
        textComponent.color = new Color(0, 0, 0, 0);
        textComponent.lineSpacing = 0;
        textComponent.alignment = TextAnchor.MiddleCenter;
        textComponent.raycastTarget = false;
        return textComponent;
    }
    private void AddTransform(RectTransform transform)
    {
        transform.localScale = new Vector3(.04f, .04f, 1);
        Vector2 newSize = new Vector2(LayoutUtility.GetPreferredWidth((RectTransform)transform),
           LayoutUtility.GetPreferredHeight((RectTransform)transform));
        transform.sizeDelta = newSize;
        transform.SetParent(Constants.CANVAS.transform);
    }
    public override void Destroy()
    {
        UnityEngine.Object.Destroy(big);
        UnityEngine.Object.Destroy(text);
    }
}
