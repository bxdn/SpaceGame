using Assets.Scripts.Controllers;
using System;
using System.Collections.Generic;

public abstract class GUIDestroyable : IGUIDestroyable
{
	public static IList<IGUIDestroyable> GUIS { get; } = new List<IGUIDestroyable>();
	public GUIDestroyable()
	{
		GUIS.Add(this);
	}
	public abstract void Destroy();
	public static void ClearGUI()
	{
		foreach (IGUIDestroyable gui in GUIS)
			gui.Destroy();
		GUIS.Clear();
		CameraController.Reset();
	}
}
