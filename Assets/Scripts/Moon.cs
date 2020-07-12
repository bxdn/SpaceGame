using System;

public class Moon : Body
{
    private static readonly int MIN_SIZE = 10;
    private static readonly int ORBITER_DELTA = 10;
    private static readonly Body[] subBodies = new Body[0];
    private readonly int size;

    public Moon(int orbiteeSize) : base()
    {
        size = ColonizerR.r.Next(MIN_SIZE, orbiteeSize - ORBITER_DELTA);
    }
    public override Body[] SubBodies => subBodies;

    public override int Size => size;
}
