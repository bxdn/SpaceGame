using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
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
            TickColonyForward(c);
        time = Time.time;
    }
    private void TickColonyForward(Colony c)
    {
        c.TickForward();
        // Update the dialog if this colony is selected
        if (Selection.CurrentSelection is ISelectable s
            && s.ModelObject is IColonizable col
            && col.ColonizableManager is IColonizableManager m
            && m.Colony == c)
            ColonyDialogController.Update(col);
    }
    public static void AddColony(Colony c)
    {
        colonies.Add(c);
    }
}
