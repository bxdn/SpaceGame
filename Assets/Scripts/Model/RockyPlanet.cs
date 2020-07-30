using Assets.Scripts;
using Assets.Scripts.Model;
using System;

public class RockyPlanet : Planet, IColonizable
{
    private static readonly int MIN_SIZE = 25;
    private static readonly int MAX_SIZE = 50;
    private static readonly int MAX_ORBITALS = 4;
    public RockyPlanet(SolarSystem sol, char id) : base(sol, id) {
            CalculateLandDivision();
            AddFields();
            Fields.Add(EField.Type, "Rocky Planet");
    }
    protected sealed override int MaxSize => MAX_SIZE;
    protected sealed override int MinSize => MIN_SIZE;
    protected sealed override int MaxOrbitals => MAX_ORBITALS;
}
