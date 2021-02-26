using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using UnityEngine;
[System.Serializable]
public class Asteroid : Orbiter, IColonizable
{
    private static readonly int MIN_SIZE = 10;
    private static readonly int MAX_SIZE = 100;
    private static readonly double ORBITER_FACTOR = .25;
    public override int Size { get; }
    public override string Name { get; set; }
    public override string Type => "Asteroid";
    public IColonizableManager ColonizableManager { get; }
    public Asteroid(SolarSystem sol, char id) : base(sol)
    {
        Name = "A-" + sol.id.ToString(Constants.FMT) + id;
        Size = ColonizerR.r.Next(MIN_SIZE, MAX_SIZE);
        ColonizableManager = new ColonizableManager(this);
    }
    public Asteroid(Planet parent, int id) : base(parent)
    {
        Size = ColonizerR.r.Next(MIN_SIZE, Math.Min(MAX_SIZE, (int)(parent.Size * ORBITER_FACTOR)));
        Name = "A" + parent.Name.Substring(1) + "-" + id;
        ColonizableManager = new ColonizableManager(this);
    }
}
