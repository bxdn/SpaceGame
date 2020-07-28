using System;
using UnityEngine;

public abstract class Planet : Orbiter, IMiddleChild
{
    protected abstract int MaxSize { get; }
    protected abstract int MinSize { get; }
    protected abstract int MaxOrbitals { get; }
    private readonly int size;
    private readonly Orbiter[] subBodies;
    private readonly String name;
    public Planet(SolarSystem sol, char id) : base(sol)
	{
        name = "P-" + sol.id.ToString(Constants.FMT) + id;
        size = ColonizerR.r.Next(MinSize, MaxSize);
        int orbitals = ColonizerR.r.Next(0, MaxOrbitals);
        subBodies = new Orbiter[orbitals];
        for (int i = 0; i < orbitals; i++)
        {
            double bodyTypeChooser = ColonizerR.r.NextDouble();
            if (bodyTypeChooser < .5)
            {
                subBodies[i] = new Asteroid(this, size, i+1);
            }
            else
            {
                subBodies[i] = new Moon(this, size, i+1);
            }
        }
    }

    public sealed override Orbiter[] SubBodies => subBodies;

    public sealed override int Size => size;

    public sealed override String Name => name;

    public IOrbitChild[] Children => subBodies;

    public void RenderSystem()
    {
        WorldGeneration.ClearGUI();
        CameraController.Reset();
        SelectorController.Reset();
        new OrbitParentGUI(this);
        foreach(IOrbitChild child in Children)
        {
            if(child is Orbiter)
            {
                new OrbiterGUI(child as Orbiter);
            }
        }
    }
}
