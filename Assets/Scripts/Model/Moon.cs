using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;

public class Moon : Orbiter, IColonizable
{
    private static readonly int MIN_SIZE = 10;
    private static readonly int MAX_SIZE = 50;
    private static readonly int ORBITER_DELTA = 10;
    public override int Size { get; }
    public override string Name { get; set; }
    public override string Type => "Moon";
    public IColonizableManager ColonizableManager { get; }
    public Moon(Planet parent, int orbiteeSize, int id) : base(parent)
    {
        Name = "M" + parent.Name.Substring(1) + "-" + id;
        Size = ColonizerR.r.Next(MIN_SIZE, Math.Min(MAX_SIZE, orbiteeSize - ORBITER_DELTA));
        ((ColonizableManager = new ColonizableManager(this)) as ColonizableManager).CalculateLandDivision();
    }

    
}
