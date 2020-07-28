using System;
using UnityEngine;

public class Asteroid : Orbiter
{
    private static readonly int MIN_SIZE = 1;
    private static readonly int MAX_SIZE = 15;
    private static readonly int ORBITER_DELTA = 20;
    private static readonly Orbiter[] subBodies = new Orbiter[0];
    public sealed override int Size { get; }
    public sealed override String Name { get; }

    public Asteroid(Planet parent, int orbiteeSize, int id) : base(parent)
    {
        Name = "A" + parent.Name.Substring(1) + "-" + id;
        Size = ColonizerR.r.Next(MIN_SIZE, Mathf.Min(orbiteeSize - ORBITER_DELTA, MAX_SIZE));
    }
    public Asteroid(SolarSystem sol, char id) : base(sol)
    {
        Name = "A-" + sol.id.ToString(Constants.FMT) + id;
        Size = ColonizerR.r.Next(MIN_SIZE, MAX_SIZE);
    }
    public override Orbiter[] SubBodies => subBodies;
}
