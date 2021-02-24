using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using System;
using UnityEngine;
[System.Serializable]
public class SolarSystem : IMiddleChild
{
    private static readonly int MAX_BODIES = 15;
    private static int count;
    public readonly int id;
    public readonly SVector2 location;

    public IChild[] Children { get; private set; }
    public bool Discovered { get; private set; } = false;

    public IParent Parent => WorldGeneration.Galaxy;

    public string Name => "S-" + id.ToString(Constants.FMT);

    public SolarSystem(SVector2 loc)
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
    public IChild DesignateStartingSystem(Galaxy g)
    {
        IColonizable startPlanet = null;
        bool startIsMoon = ColonizerR.r.Next(100) < 50;
        while (startPlanet == null)
        {
            Discovered = false;
            Discover();
            if (startIsMoon)
            {
                foreach (IChild body in Children)
                {
                    if (body is IParent parent)
                    {
                        foreach (IChild orbiter in parent.Children)
                        {
                            if (orbiter is Moon moon && moon.DesignateStartingWorld(g))
                            {
                                startPlanet = moon;
                                break;
                            }
                        }
                    }
                    if (startPlanet != null)
                    {
                        break;
                    }
                }
            }
            else
            {
                foreach (IChild body in Children)
                {
                    if (body is RockyPlanet planet && planet.DesignateStartingWorld(g))
                    {
                        startPlanet = planet;
                        break;
                    }
                }
            }
        }
        return startPlanet;
    }

    public void RenderSystem()
    {
        Discover();
        GUIDestroyable.ClearGUI();
        CameraController.Reset();
        SelectorController.Reset();
        WorldGeneration.Galaxy.CurrentSystem = this;
        new OrbitParentGUI(this);
        foreach (IChild child in Children)
        {
            if (child is Orbiter orbiter)
            {
                new OrbiterGUI(orbiter);
            }
        }
    }
}
