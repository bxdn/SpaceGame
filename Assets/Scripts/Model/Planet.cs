using Assets.Scripts.Model;
using System;
using UnityEngine;

public abstract class Planet : Orbiter, IMiddleChild
{
    protected abstract int MaxSize { get; }
    protected abstract int MinSize { get; }
    protected abstract int MaxOrbitals { get; }
    protected sealed override int Size { get; }
    public sealed override String Name { get; }
    public IOrbitChild[] Children { get; }
    public sealed override IOrbitChild[] SubBodies => Children;
    public Planet(SolarSystem sol, char id) : base(sol)
    {
        Name = "P-" + sol.id.ToString(Constants.FMT) + id;
        Size = ColonizerR.r.Next(MinSize, MaxSize);
        int orbitals = ColonizerR.r.Next(0, MaxOrbitals);
        Children = new Orbiter[orbitals];
        for (int i = 0; i < orbitals; i++)
        {
            if(ColonizerR.r.NextDouble() < .5)
            {
                Children[i] = new Moon(this, Size, i + 1);
            }
            else
            {
                Children[i] = new Asteroid(this, Size, i + 1);
            }
        }
    }
    public void RenderSystem()
    {
        WorldGeneration.ClearGUI();
        CameraController.Reset();
        new OrbitParentGUI(this);
        foreach(IOrbitChild child in Children)
        {
            if(child is Orbiter orbiter)
            {
                new OrbiterGUI(orbiter);
            }
        }
    }
}
