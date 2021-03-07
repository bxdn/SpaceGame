using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GUIScrollable : GUIDestroyable
{
	public GUIScrollable() : base()
	{
	}
	public abstract void Scroll(bool ascending);
}
