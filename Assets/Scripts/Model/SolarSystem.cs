using System;
using UnityEngine;

public class SolarSystem : IMiddleChild
{
    private static readonly int MAX_BODIES = 15;
    private static int count;
    public readonly int id;
    public readonly Vector2 location;

    public IOrbitChild[] Children { get; private set; }
    public bool Discovered { get; private set; } = false;

    public IOrbitParent Parent => WorldGeneration.Galaxy;

    public string Name => "S-" + id.ToString(Constants.FMT);

    public SolarSystem(Vector2 loc)
	{
        id = count++;
        location = loc;
	}

    private void Discover()
    {
        if (Discovered)
        {
            return;
        }
        Discovered = true;
        int numBodies = ColonizerR.r.Next(0, MAX_BODIES);
        Children = new Orbiter[numBodies];
        const int CHARACTER = 65;
        for (int i = 0; i < numBodies; i++)
        {
            char identifier = (char) (CHARACTER + i);
            double bodyTypeChooser = ColonizerR.r.NextDouble();
            if (bodyTypeChooser < .33)
            {
                Children[i] = new Asteroid(this, identifier);
            }
            else if (bodyTypeChooser < .66)
            {
                Children[i] = new RockyPlanet(this, identifier);
            }
            else
            {
                Children[i] = new GasGiant(this, identifier);
            }
        }
    }
    public RockyPlanet DesignateStartingSystem()
    {
        RockyPlanet startPlanet = null;
        while (startPlanet == null)
        {
            Discovered = false;
            Discover();
            foreach(Orbiter body in Children)
            {
                if (body is RockyPlanet r)
                {
                    startPlanet = r;
                    break;
                }
            }
        }
        return startPlanet;
    }

    public void RenderSystem()
    {
        Discover();
        WorldGeneration.ClearGUI();
        CameraController.Reset();
        SelectorController.Reset();
        WorldGeneration.Galaxy.CurrentSystem = this;
        new OrbitParentGUI(this);
        foreach (IOrbitChild child in Children)
        {
            if (child is Orbiter orbiter)
            {
                new OrbiterGUI(orbiter);
            }
        }
    }
}
