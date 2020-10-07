using Assets.Scripts.Controllers;
using System;
using System.Collections.Generic;

public abstract class GUIDestroyable : IGUIDestroyable
{
	public static readonly IList<IGUIDestroyable> guis = new List<IGUIDestroyable>();
	public GUIDestroyable()
	{
		guis.Add(this);
	}
	public abstract void Destroy();
	public static void ClearGUI()
	{
		foreach (IGUIDestroyable gui in guis)
		{
			gui.Destroy();
		}
		guis.Clear();
	}
}
