using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Galaxy : IOrbitParent
{
    private static readonly int NUM_STARS = 1000;
    private static readonly int REACHABLE_DISTANCE_SQUARED = 300;
    private static readonly int MIN_STAR_SPACING_SQUARED = 100;
    private static readonly int MAP_SIZE = 500;
    public readonly ISet<SolarSystem> nodes = new HashSet<SolarSystem>();
    public readonly IDictionary<SolarSystem, ISet<SolarSystem>> reachableStars =
        new Dictionary<SolarSystem, ISet<SolarSystem>>();
    public readonly SolarSystem startingSystem;
    public readonly IOrbitChild startingWorld;
    public SolarSystem CurrentSystem { get; set; }

    public IOrbitChild[] Children => nodes.ToArray();

    public string Name => null;

    public Galaxy()
    {
        CreateSystems();
        CreatePaths();
        Tuple<SolarSystem, IOrbitChild> start = GetStartingSystem();
        startingSystem = start.Item1;
        startingWorld = start.Item2;
    }

    private void CreateSystems()
    {
        ISet<Vector2> locations = new HashSet<Vector2>();
        for (int i = 0; i < NUM_STARS; i++)
        {
            Vector2 location = Vector2.zero;
            bool acceptable = false;
            while (!acceptable)
            {
                bool unacceptable = false;
                location = new Vector2((float)ColonizerR.r.NextDouble() * MAP_SIZE, (float)ColonizerR.r.NextDouble() * MAP_SIZE);
                foreach (Vector2 loc in locations)
                {
                    float deltaX = loc.x - location.x;
                    float deltaY = loc.y - location.y;
                    float distSq = deltaX * deltaX + deltaY * deltaY;
                    if (distSq < MIN_STAR_SPACING_SQUARED)
                    {
                        unacceptable = true;
                        break;
                    }
                }
                if (!unacceptable)
                {
                    locations.Add(location);
                    acceptable = true;
                }
            }
            nodes.Add(new SolarSystem(location));
        }
    }
    private void CreatePaths()
    {
        foreach (SolarSystem star in nodes)
        {
            ISet<SolarSystem> reachable = new HashSet<SolarSystem>();
            reachableStars[star] = reachable;
            float x = star.location.x;
            float y = star.location.y;
            foreach (SolarSystem node in nodes)
            {
                if (node == star)
                {
                    continue;
                }
                float deltaX = node.location.x - x;
                float deltaY = node.location.y - y;
                float distSq = deltaX * deltaX + deltaY * deltaY;
                if (distSq <= REACHABLE_DISTANCE_SQUARED)
                {
                    reachable.Add(node);
                }
            }
        }
    }

    private Tuple<SolarSystem,IOrbitChild> GetStartingSystem()
    {
        int systemCount = nodes.Count;
        int systemIdx = ColonizerR.r.Next(systemCount);
        int curIdx = 0;
        foreach (SolarSystem node in nodes)
        {
            if (curIdx++ == systemIdx)
            {
                IOrbitChild startWorld = node.DesignateStartingSystem();
                return new Tuple<SolarSystem, IOrbitChild>(node, startWorld);
            }
        }
        return null;
    }

    public void RenderStartingSystem()
    {
        if(startingWorld is Planet p)
        {
            p.RenderSystem();
        }
        else
        {
            startingWorld.Parent.RenderSystem();
        }
    }

    public void RenderSystem()
    {
        WorldGeneration.ClearGUI();
        SelectorController.Reset();
        ISet<SolarSystem> finishedNodes = new HashSet<SolarSystem>();
        foreach (SolarSystem node in nodes)
        {
            new SystemGUI(node);
            foreach (SolarSystem adjNode in reachableStars[node])
            {
                if (!finishedNodes.Contains(adjNode))
                {
                    new PathGUI(node.location, adjNode.location);
                }
            }
            finishedNodes.Add(node);
        }
        if(CurrentSystem == null)
        {
            CameraController.Reset();
        }
        else
        {
            CameraController.Reset(CurrentSystem.location);
        }
    }
}
