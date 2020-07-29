using Assets.Scripts;
using System;
using UnityEngine;

public class Asteroid : Orbiter, IColonizable
{
    private static readonly int MIN_SIZE = 1;
    private static readonly int MAX_SIZE = 20;
    private static readonly Orbiter[] subBodies = new Orbiter[0];
    public sealed override int Size { get; }
    public sealed override string Name { get; }

    public Asteroid(SolarSystem sol, char id) : base(sol)
    {
        Name = "A-" + sol.id.ToString(Constants.FMT) + id;
        Size = ColonizerR.r.Next(MIN_SIZE, MAX_SIZE);
    }
    public override Orbiter[] SubBodies => subBodies;

    public Domain Owner { get; set; }
}
