using Assets.Scripts.Interfaces;
using System;

public interface IOrbitChild : INamable, IModelObject
{
	IOrbitParent Parent { get; }
}
