using System;
using UnityEngine;

public abstract class Body
{
    private static readonly int MAX_DIST = 100;
    private static readonly float MAX_ANGLE = 2 * Mathf.PI;
    public readonly int distance;
    public readonly float initialAngle;
    public abstract Body[] SubBodies { get; }
    public abstract int Size { get; }
	public Body()
	{
        distance = ColonizerR.r.Next(MAX_DIST);
        initialAngle = (float) ColonizerR.r.NextDouble() * MAX_ANGLE;
	}
}
