using Assets.Scripts;
using System;

public class Moon : Orbiter, IColonizable
{
    private static readonly int MIN_SIZE = 5;
    private static readonly int ORBITER_DELTA = 10;
    private static readonly Orbiter[] subBodies = new Orbiter[0];
    public sealed override int Size { get; }
    public sealed override String Name { get; }

    public Moon(Planet parent, int orbiteeSize, int id) : base(parent)
    {
        Name = "M" + parent.Name.Substring(1) + "-" + id;
        Size = ColonizerR.r.Next(MIN_SIZE, orbiteeSize - ORBITER_DELTA);
    }

    public override Orbiter[] SubBodies => subBodies;

    public Domain Owner { get; set; }
}
