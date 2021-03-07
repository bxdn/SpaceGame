using Assets.Scripts.Model;
using System;
using UnityEngine;
[System.Serializable]
public abstract class Planet : Orbiter, IMiddleChild
{
    protected abstract int MaxSize { get; }
    protected abstract int MinSize { get; }
    protected abstract int MaxOrbitals { get; }
    private readonly IChild[] children;
    public IChild[] Children
    {
        get => (IChild[]) children.Clone();
    }
    public override int Size { get; }
    public override String Name { get; set; }
    public Planet(SolarSystem sol, char id) : base(sol)
    {
        Name = "P-" + sol.id.ToString(Constants.FMT) + id;
        Size = ColonizerR.r.Next(MinSize, MaxSize);
        int orbitals = ColonizerR.r.Next(0, MaxOrbitals);
        children = new Orbiter[orbitals];
        for (int i = 0; i < orbitals; i++)
        {
            if(ColonizerR.r.NextDouble() < .5)
            {
                children[i] = new Moon(this, i + 1);
            }
            else
            {
                children[i] = new Asteroid(this, i + 1);
            }
        }
    }
    public void RenderSystem()
    {
        GUIDestroyable.ClearGUI();
        CameraController.Reset();
        new OrbitParentGUI(this);
        foreach(IChild child in Children)
        {
            if(child is Orbiter orbiter)
            {
                new OrbiterGUI(orbiter);
            }
        }
    }
}
