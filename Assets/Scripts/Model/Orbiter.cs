﻿using Assets.Scripts;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Orbiter : IOrbitChild
{
    private static readonly int MAX_DIST = 100;
    private static readonly float MAX_ANGLE = 2 * Mathf.PI;
    public int Distance { get; }
    public float InitialAngle { get; }
    public IOrbitParent Parent { get; }
    public abstract int Size { get; }
    public abstract String Name { get; set; }
    public abstract String Type { get; }
    public Orbiter(IOrbitParent parent)
	{
        Distance = ColonizerR.r.Next(MAX_DIST);
        InitialAngle = (float)ColonizerR.r.NextDouble() * MAX_ANGLE;
        Parent = parent;  
    }
}
