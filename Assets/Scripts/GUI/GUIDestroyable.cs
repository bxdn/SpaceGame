using System;

public abstract class GUIDestroyable : IGUIDestroyable
{
	public GUIDestroyable()
	{
		WorldGeneration.guis.Add(this);
	}

	public abstract void Destroy();
}
