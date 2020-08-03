using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;

public class RockyPlanet : Planet, IArable
{
    public override string Type => "Rocky Planet";
    protected sealed override int MaxSize => 50;
    protected sealed override int MinSize => 25;
    protected sealed override int MaxOrbitals => 4;
    public IColonizableManager ColonizableManager { get; }
    public RockyPlanet(SolarSystem sol, char id) : base(sol, id)
    {
        ColonizableManager = new ColonizableManager(this);
    }
}
