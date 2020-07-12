using System;
using UnityEngine;

public class SystemGUI : GUIDestroyable
{
    private readonly GameObject star;
	public SystemGUI(SolarSystem system)
	{
        star = new GameObject("System");
        Transform starTransform = star.transform;
        starTransform.localPosition = new Vector3(system.location.x, system.location.y, 0);
        starTransform.localScale = new Vector3(2, 2, 1);
        starTransform.parent = Constants.GRID.transform;
        SpriteRenderer starRenderer = star.AddComponent<SpriteRenderer>();
        starRenderer.sprite = Constants.circle;
        starRenderer.color = Color.white;
        starRenderer.sortingOrder = 1;
        star.AddComponent<CircleCollider2D>();
        SystemGUIController controller = star.AddComponent<SystemGUIController>();
        controller.system = system;
    }

    public void Destroy()
    {
        UnityEngine.Object.Destroy(star);
    }
}
