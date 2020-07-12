using System;

public class GasGiant : Planet
{
    private static readonly int MIN_SIZE = 80;
    private static readonly int MAX_SIZE = 100;
    private static readonly int MAX_ORBITALS = 10;

    protected override int MaxSize => MAX_SIZE;

    protected override int MinSize => MIN_SIZE;

    protected override int MaxOrbitals => MAX_ORBITALS;
}
