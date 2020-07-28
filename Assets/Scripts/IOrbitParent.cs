using System;

public interface IOrbitParent : INamable
{
	IOrbitChild[] Children { get; }

	void RenderSystem();
}
