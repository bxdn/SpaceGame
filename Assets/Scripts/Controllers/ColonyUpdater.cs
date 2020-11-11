using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using UnityEngine;

public class ColonyUpdater : MonoBehaviour
{
    private bool paused = false;
    private float time = -1;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            paused = !paused;
        if (!paused && WorldGeneration.Galaxy != null && Time.time - time > 1)
            TickForward();
    }
    private void TickForward()
    {
        foreach (Colony c in WorldGeneration.Galaxy.Player.Colonies)
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
            UpdateGUIS(col);
    }
    private void UpdateGUIS(IColonizable c)
    {
        ColonyDialogController.Update(c);
        GoodsDialogController.Update(c);
    }
    public static void AddColony(Colony c, Galaxy g)
    {
        g.Player.Colonies.Add(c);
    }
}
