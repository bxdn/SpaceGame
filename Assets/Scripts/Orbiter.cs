using System;
using UnityEngine;

public abstract class Orbiter : IOrbitChild
{
    private static readonly int MAX_DIST = 100;
    private static readonly float MAX_ANGLE = 2 * Mathf.PI;
    public readonly int distance;
    public readonly float initialAngle;

    public abstract Orbiter[] SubBodies { get; }
    public abstract int Size { get; }

    public IOrbitParent Parent { get; }

    public abstract string Name { get; }

    public Orbiter(IOrbitParent parent)
	{
        distance = ColonizerR.r.Next(MAX_DIST);
        initialAngle = (float)ColonizerR.r.NextDouble() * MAX_ANGLE;
        Parent = parent;
	}
}
