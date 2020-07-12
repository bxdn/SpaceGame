using System;

public abstract class Planet : Body
{
    protected abstract int MaxSize { get; }
    protected abstract int MinSize { get; }
    protected abstract int MaxOrbitals { get; }
    private readonly int size;
    private readonly Body[] subBodies;
    public Planet()
	{
        size = ColonizerR.r.Next(MinSize, MaxSize);
        int orbitals = ColonizerR.r.Next(0, MaxOrbitals);
        subBodies = new Body[orbitals];
        for (int i = 0; i < orbitals; i++)
        {
            double bodyTypeChooser = ColonizerR.r.NextDouble();
            if (bodyTypeChooser < .5)
            {
                subBodies[i] = new Asteroid(size);
            }
            else
            {
                subBodies[i] = new Moon(size);
            }
        }
    }

    public sealed override Body[] SubBodies => subBodies;

    public sealed override int Size => size;
}
