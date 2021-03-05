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
            TickColonyForward(c);
        time = Time.time;
    }
    private void TickColonyForward(Colony c)
    {
        c.TickForward();
        // Update the dialog if this colony is selected
        if (Selection.CurrentSelection is IColonizable col
            && col.ColonizableManager is IColonizableManager m
            && m.CurrentColony == c)
            UpdateGUIS(c);
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
