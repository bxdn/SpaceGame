using Assets.Scripts.Interfaces;
using System;

public interface IChild : INamable, IModelObject
{
	IParent Parent { get; }
}
