using System;

public interface IOrbitChild : INamable
{
	IOrbitParent Parent { get; }
}
