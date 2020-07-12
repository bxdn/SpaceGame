using System;
using UnityEngine;

public class Asteroid : Body
{
    private static readonly int MIN_SIZE = 1;
    private static readonly int MAX_SIZE = 15;
    private static readonly int ORBITER_DELTA = 20;
    private static readonly Body[] subBodies = new Body[0];
    private readonly int size;

    public Asteroid(int orbiteeSize) : base()
    {
        size = ColonizerR.r.Next(MIN_SIZE, Mathf.Min(orbiteeSize - ORBITER_DELTA, MAX_SIZE));
    }
    public Asteroid() : base()
    {
        size = ColonizerR.r.Next(MIN_SIZE, MAX_SIZE);
    }
    public override Body[] SubBodies => subBodies;

    public override int Size => size;
}
