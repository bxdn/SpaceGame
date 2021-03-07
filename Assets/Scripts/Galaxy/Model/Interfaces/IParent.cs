using System;

public interface IParent : INamable
{
	IChild[] Children { get; }

	void RenderSystem();
}
