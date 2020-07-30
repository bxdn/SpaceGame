using Assets.Scripts;
using Assets.Scripts.Model;
using System;

public class GasGiant : Planet
{
    private static readonly int MIN_SIZE = 80;
    private static readonly int MAX_SIZE = 100;
    private static readonly int MAX_ORBITALS = 10;

    protected override int MaxSize => MAX_SIZE;

    protected override int MinSize => MIN_SIZE;

    protected override int MaxOrbitals => MAX_ORBITALS;

    public sealed override Domain Owner { get => base.Owner; set => throw new InvalidOperationException(); }

    public GasGiant(SolarSystem sol, char id) : base(sol, id) {
        AddFields();
        Fields.Add(EField.Type, "Gas Giant");
        Fields[EField.ArableLand] = "N/A";
        Fields[EField.OtherUsableLand] = "N/A";
        Fields[EField.Colonized] = "N/A";
        Fields[EField.HazardFrequency] = "N/A";
    }
}
