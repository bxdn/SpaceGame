using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public static Galaxy Galaxy { get; private set; }
    public static IList<GUIDestroyable> guis = new List<GUIDestroyable>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting Galaxy Generation!");
        Galaxy = new Galaxy();
        Debug.Log("Finished Galaxy Generation!");
        //RenderGalaxy(Galaxy);
        RenderSystem(Galaxy.startingSystem);
    }

    public static void RenderGalaxy(SolarSystem system)
    {
        ClearGUI();
        ISet<SolarSystem> finishedNodes = new HashSet<SolarSystem>();
        foreach(SolarSystem node in Galaxy.nodes)
        {
            guis.Add(new SystemGUI(node));
            foreach(SolarSystem adjNode in Galaxy.reachableStars[node])
            {
                if (!finishedNodes.Contains(adjNode))
                {
                    guis.Add(new PathGUI(node.location, adjNode.location));
                }
            }
            finishedNodes.Add(node);
        }
        if(system == null)
        {
            CameraController.Reset();
        }
        else
        {
            CameraController.Reset(system.location);
        }
        SelectorController.Reset();
    }

    private static void ClearGUI()
    {
        foreach(GUIDestroyable gui in guis)
        {
            gui.Destroy();
        }
    }

    private static SolarSystem GetRandomSolarSystem()
    {
        int systemCount = Galaxy.nodes.Count;
        int systemIdx = ColonizerR.r.Next(systemCount);
        int curIdx = 0;
        foreach(SolarSystem node in Galaxy.nodes)
        {
            if(curIdx++ == systemIdx)
            {
                return node;
            }
        }
        return null;
    }

    public static void RenderSystem(SolarSystem sol)
    {
        ClearGUI();
        sol.Discover();
        int character = 65;
        String numStr = sol.id.ToString(Constants.FMT);
        guis.Add(new StarGUI(sol));
        foreach (Body body in sol.Bodies)
        {
            float initX = body.distance * Mathf.Cos(body.initialAngle);
            float initY = body.distance * Mathf.Sin(body.initialAngle);
            guis.Add(new PlanetGUI(new Vector2(initX, initY), "P-" + numStr + (char)character));
            character++;
        }
        CameraController.Reset();
        SelectorController.Reset();
    }
}
