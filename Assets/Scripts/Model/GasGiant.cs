using Assets.Scripts;
using Assets.Scripts.Model;
using System;

public class GasGiant : Planet
{
    protected override int MaxSize => 100;
    protected override int MinSize => 80;
    protected override int MaxOrbitals => 10;
    public override string Type { get; }
    public GasGiant(SolarSystem sol, char id) : base(sol, id) {
        Type = "Gas Giant";
    }
}
