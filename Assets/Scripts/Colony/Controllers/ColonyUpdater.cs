using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using UnityEngine;

public class ColonyUpdater : MonoBehaviour
{
    private static bool paused = true;
    private float time = -1;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TogglePause();
        if (!paused && WorldGeneration.Galaxy != null && Time.time - time > 1)
            TickForward();
    }
    public static void TogglePause()
    {
        paused = !paused;
        PlayController.ToggleText();
    }
    private void TickForward()
    {
        foreach (Colony c in WorldGeneration.Galaxy.Player.Colonies)
            c.TickForward();
        foreach (Colony c in WorldGeneration.Galaxy.Player.Colonies)
            c.FinishGoodsCalculations();
        if (Selection.CurrentSelection is IColonizable col
            && col.ColonizableManager is IColonizableManager m)
            UpdateGUIS(m.CurrentColony);
        time = Time.time;
    }
    private void UpdateGUIS(Colony c)
    {
        ColonyDialogController.Update(c);
        GoodsDialogController.Update(c);
    }
    public static void AddColony(Colony c)
    {
        WorldGeneration.Galaxy.Player.Colonies.Add(c);
    }
}
