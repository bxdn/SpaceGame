﻿using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
[System.Serializable]
public class RockyPlanet : Planet, IArable
{
    public override string Type => "Rocky Planet";
    protected sealed override int MaxSize => 5000;
    protected sealed override int MinSize => 500;
    protected sealed override int MaxOrbitals => 4;
    public IColonizableManager ColonizableManager { get; private set; }
    public RockyPlanet(SolarSystem sol, char id) : base(sol, id)
    {
        ColonizableManager = new ColonizableManager(this);
    }

    public bool DesignateStartingWorld(Galaxy g)
    {
        return ((ColonizableManager = new StartingWorldColonizableManager(this)) as StartingWorldColonizableManager).DesignateStartingColony(g);
    }
}
