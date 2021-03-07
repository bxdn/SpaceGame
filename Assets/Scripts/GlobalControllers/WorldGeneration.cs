using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

public class WorldGeneration : MonoBehaviour
{
    public static Galaxy Galaxy { get; private set; }
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        if (Galaxy == null)
            StartWorld();
    }
    public static void StartWorld()
    {
        Debug.Log("Starting Galaxy Generation!");
        Galaxy = SaveUtility.SavedGameFound() ? SaveUtility.LoadGame() : new Galaxy();
        Debug.Log("Finished Galaxy Generation!");
        Galaxy.RenderStartingSystem();
    }
}
