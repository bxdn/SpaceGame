using Assets.Scripts;
using System;
using UnityEngine;

public class SystemGUI : GUIDestroyable
{
    private readonly GameObject star;
    private readonly SolarSystem solarSystem;
	public SystemGUI(SolarSystem system): base()
	{
        star = new GameObject("System");
        solarSystem = system;
        Transform starTransform = star.transform;
        starTransform.localPosition = new Vector3(system.location.x, system.location.y, 0);
        starTransform.localScale = new Vector3(3, 3, 1);
        starTransform.parent = Constants.GRID.transform;
        SpriteRenderer starRenderer = star.AddComponent<SpriteRenderer>();
        starRenderer.sprite = Constants.circle;
        starRenderer.color = GetColor();
        starRenderer.sortingOrder = 1;
        star.AddComponent<CircleCollider2D>();
        SystemGUIController controller = star.AddComponent<SystemGUIController>();
        controller.system = system;
    }

    private Color GetColor()
    {
        if (!solarSystem.Discovered)
        {
            return Color.grey;
        }
        foreach (IOrbitChild child in solarSystem.Children)
        {
            if (child is IColonizableManager colonizable && colonizable.Owner == Player.Domain)
            {
                return Constants.colonizedColor;
            }
            else if (child is IOrbitParent parent)
            {
                foreach (IOrbitChild leaf in parent.Children)
                {
                    if (leaf is IColonizableManager colonizableLeaf && colonizableLeaf.Owner == Player.Domain)
                    {
                        return Constants.colonizedColor;
                    }
                }
            }
        }
        return Color.white;
    }

    public override void Destroy()
    {
        UnityEngine.Object.Destroy(star);
    }
}
