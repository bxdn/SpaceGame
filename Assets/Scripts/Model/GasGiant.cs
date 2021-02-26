using Assets.Scripts;
using Assets.Scripts.Model;
using System;
[System.Serializable]
public class GasGiant : Planet
{
    protected override int MaxSize => 100000;
    protected override int MinSize => 7500;
    protected override int MaxOrbitals => 10;
    public override string Type => "Gas Giant";
    public GasGiant(SolarSystem sol, char id) : base(sol, id) { }
}
