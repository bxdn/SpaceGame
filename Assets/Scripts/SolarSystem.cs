using System;
using UnityEngine;

public class SolarSystem
{
    private static readonly int MAX_BODIES = 15;
    private static int count;
    public readonly int id;
    public readonly Vector2 location;

    public Body[] Bodies { get; private set; }
    public bool Discovered { get; private set; } = false;
    public SolarSystem(Vector2 loc)
	{
        id = count++;
        location = loc;
	}

    public void Discover()
    {
        if (Discovered)
        {
            return;
        }
        Discovered = true;
        int numBodies = ColonizerR.r.Next(0, MAX_BODIES);
        Bodies = new Body[numBodies];
        for (int i = 0; i < numBodies; i++)
        {
            double bodyTypeChooser = ColonizerR.r.NextDouble();
            if (bodyTypeChooser < .33)
            {
                Bodies[i] = new Asteroid();
            }
            else if (bodyTypeChooser < .66)
            {
                Bodies[i] = new RockyPlanet();
            }
            else
            {
                Bodies[i] = new GasGiant();
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
            foreach(Body body in Bodies)
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
}
