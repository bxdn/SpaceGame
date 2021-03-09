using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

public class SystemGUI : GUIDestroyable
{
    private GameObject star;
    private SolarSystem solarSystem;
	public SystemGUI(SolarSystem system): base()
	{
        SetMembers(system);
        CreateTransform(star.transform, system);
        CreateRenderer(star.AddComponent<SpriteRenderer>());
        star.AddComponent<SystemGUIController>().system = system;
    }
    private void SetMembers(SolarSystem system)
    {
        star = new GameObject("System");
        solarSystem = system;
    }
    private void CreateTransform(Transform starTransform, SolarSystem system)
    {
        starTransform.localPosition = new Vector3(system.location.x, system.location.y, 0);
        starTransform.localScale = new Vector3(3, 3, 1);
        starTransform.parent = Constants.GRID.transform;
    }
    private void CreateRenderer(SpriteRenderer starRenderer)
    {
        starRenderer.sprite = Constants.circle;
        starRenderer.color = GetColor();
        starRenderer.sortingOrder = 1;
        star.AddComponent<CircleCollider2D>();
    }
    private Color GetColor()
    {
        if (!solarSystem.Discovered)
            return Color.grey;
        Color? toRet = null;
        for (int i = 0; i < solarSystem.Children.Length && toRet == null; i++)
            toRet = GetColor(solarSystem.Children[i]);
        return toRet == null ? Color.white : (Color) toRet;
    }
    private Color? GetColor(IChild child)
    {
        if (IsPlayerColonized(child))
            return Constants.colonizedColor;
        if (child is IParent parent)
            return GetColor(parent);
        return null;
    }
    private Color? GetColor(IParent parent)
    {
        Color? toRet = null;
        for (int i = 0; i < parent.Children.Length && toRet == null; i++)
            toRet = IsPlayerColonized(parent.Children[i]) ? (Color?) Constants.colonizedColor : null;
        return toRet;
    }
    private bool IsPlayerColonized(IChild child)
    {
        return child is IColonizable colonizable && 
            colonizable.ColonizableManager.Owner == WorldGeneration.Galaxy.Player.Domain;
    }

    public override void Destroy()
    {
        UnityEngine.Object.Destroy(star);
    }
}
