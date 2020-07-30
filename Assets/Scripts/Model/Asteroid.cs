using Assets.Scripts;
using Assets.Scripts.Model;
using System;
using UnityEngine;

public class Asteroid : Orbiter, IColonizable
{
    private static readonly int MIN_SIZE = 1;
    private static readonly int MAX_SIZE = 15;
    private static readonly int ORBITER_DELTA = 10;
    protected sealed override int Size { get; }
    public sealed override string Name { get; }
    public override IOrbitChild[] SubBodies { get; }
    public Asteroid(SolarSystem sol, char id) : base(sol)
    {
        SubBodies = new IOrbitChild[0];
        Name = "A-" + sol.id.ToString(Constants.FMT) + id;
        Size = ColonizerR.r.Next(MIN_SIZE, MAX_SIZE);
        Initialize();
    }
    public Asteroid(Planet parent, int orbiteeSize, int id) : base(parent)
    {
        SubBodies = new IOrbitChild[0];
        Size = ColonizerR.r.Next(MIN_SIZE, orbiteeSize - ORBITER_DELTA);
        Name = "A" + parent.Name.Substring(1) + "-" + id;
        Initialize();
    }
    private void Initialize()
    {
        OtherLand = ColonizerR.r.Next(0, Size);
        AddFields();
        Fields.Add(EField.Type, "Asteroid");
    }
}
