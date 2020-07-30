using Assets.Scripts;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Orbiter : IOrbitChild, IColonizable
{
    private static readonly int MAX_DIST = 100;
    private static readonly float MAX_ANGLE = 2 * Mathf.PI;
    public readonly int distance;
    public readonly float initialAngle;
    private Domain owner;
    public IDictionary<EField, String> Fields { get; } = new Dictionary<EField, String>();

    public abstract IOrbitChild[] SubBodies { get; }
    protected abstract int Size { get; }

    protected int ArrableLand { get; set; } = 0;

    protected int OtherLand { get; set; } = 0;

    protected int HazardFrequency { get; }

    public IOrbitParent Parent { get; }

    public abstract string Name { get; }
    public virtual Domain Owner
    {
        get => owner;
        set
        {
            Fields[EField.Colonized] = "Yes";
            owner = value;
        }
    }

    public Orbiter(IOrbitParent parent)
	{
        distance = ColonizerR.r.Next(MAX_DIST);
        initialAngle = (float)ColonizerR.r.NextDouble() * MAX_ANGLE;
        Parent = parent;
        HazardFrequency = ColonizerR.r.Next(100);
    }

    protected void AddFields()
    {
        Fields.Add(EField.Name, Name);
        Fields.Add(EField.Size, Size.ToString());
        Fields.Add(EField.Colonized, "No");
        Fields.Add(EField.OrbitingBodies, SubBodies.Length.ToString());
        Fields.Add(EField.ArableLand, ArrableLand.ToString());
        Fields.Add(EField.OtherUsableLand, OtherLand.ToString());
        Fields.Add(EField.HazardFrequency, HazardFrequency.ToString());
    }

    protected void CalculateLandDivision()
    {
        ArrableLand =  ColonizerR.r.Next(0, Size);
        OtherLand = ColonizerR.r.Next(0, Size - ArrableLand);
    }
}
