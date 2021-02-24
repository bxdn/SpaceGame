using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Orbiter : IBody
{
    private static readonly int MAX_DIST = 100;
    private static readonly int MIN_DIST = 10;
    private static readonly float MAX_ANGLE = 2 * Mathf.PI;
    public int Distance { get; }
    public float InitialAngle { get; }
    public IParent Parent { get; }
    public abstract int Size { get; }
    public abstract String Name { get; set; }
    public abstract String Type { get; }
    public Orbiter(IParent parent)
	{
        Distance = ColonizerR.r.Next(MIN_DIST, MAX_DIST);
        InitialAngle = (float)ColonizerR.r.NextDouble() * MAX_ANGLE;
        Parent = parent;  
    }
}
