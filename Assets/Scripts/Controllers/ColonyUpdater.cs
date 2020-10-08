using Assets.Scripts.Model;
using System.Collections.Generic;
using UnityEngine;

public class ColonyUpdater : MonoBehaviour
{
    private static ISet<Colony> colonies = new HashSet<Colony>();
    private float time = -1;
    // Update is called once per frame
    void Update()
    {
        if (Time.time - time > 1)
            TickForward();
    }
    private void TickForward()
    {
        foreach (Colony c in colonies)
            c.TickForward();
        time = Time.time;
    }
    public static void AddColony(Colony c)
    {
        colonies.Add(c);
    }
}
