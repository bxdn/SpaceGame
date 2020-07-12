using System;

public class RockyPlanet : Planet
{
    private static readonly int MIN_SIZE = 25;
    private static readonly int MAX_SIZE = 75;
    private static readonly int MAX_ORBITALS = 4;
    public RockyPlanet() : base()
    {

    }
    protected sealed override int MaxSize => MAX_SIZE;

    protected sealed override int MinSize => MIN_SIZE;

    protected sealed override int MaxOrbitals => MAX_ORBITALS;
}
