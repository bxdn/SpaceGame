using Assets.Scripts;
using Assets.Scripts.Model;
using System;

public class Moon : Orbiter, IColonizable
{
    private static readonly int MIN_SIZE = 10;
    private static readonly int MAX_SIZE = 50;
    private static readonly int ORBITER_DELTA = 10;
    protected sealed override int Size { get; }
    public sealed override String Name { get; }
    public override IOrbitChild[] SubBodies { get; }
    public Moon(Planet parent, int orbiteeSize, int id) : base(parent)
    {
        SubBodies = new Orbiter[0];
        Name = "M" + parent.Name.Substring(1) + "-" + id;
        Size = ColonizerR.r.Next(MIN_SIZE, Math.Min(MAX_SIZE, orbiteeSize - ORBITER_DELTA));
        CalculateLandDivision();
        AddFields();
        Fields.Add(EField.Type, "Moon");
    }


}
