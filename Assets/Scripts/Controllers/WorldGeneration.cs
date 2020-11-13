using Assets.Scripts.Model;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public static Galaxy Galaxy { get; private set; }
    //private GameObject[] whiteSquares = new GameObject[10000];
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;
        StartWorld();
        /*int idx = 0;
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                Vector2 loc = new Vector2(5 * i, 5 * j);
                GameObject bigSquare = new GameObject("Square");
                whiteSquares[idx++] = bigSquare;
                bigSquare.transform.parent = Constants.GRID.transform;
                var renderer = bigSquare.AddComponent<SpriteRenderer>();
                renderer.sprite = Constants.square;
                renderer.color = Color.white;
                bigSquare.transform.position = loc;
                bigSquare.transform.localScale = new Vector2(5, 5);
                GameObject smallSquare = new GameObject("Square");
                smallSquare.transform.parent = Constants.GRID.transform;
                var smallRenderer = smallSquare.AddComponent<SpriteRenderer>();
                smallRenderer.sprite = Constants.square;
                var rand = ColonizerR.r.Next(100);
                smallRenderer.color = rand < 10 ? Color.white : Color.black;
                smallRenderer.sortingOrder = 1;
                smallSquare.transform.position = loc;
                smallSquare.transform.localScale = new Vector2(4.7f, 4.7f);
            }
        }*/
    }
    public static void StartWorld()
    {
        Debug.Log("Starting Galaxy Generation!");
        Galaxy = Save.SavedGameFound() ? Save.LoadGame() : new Galaxy();
        Debug.Log("Finished Galaxy Generation!");
        Galaxy.RenderStartingSystem();
    }
    /*void Update()
    {
        var pos = CameraController.Camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Debug.Log(pos);
        foreach (GameObject square in whiteSquares)
        {
            float distance = Mathf.Sqrt(Mathf.Pow(square.transform.position.x - pos.x, 2) + Mathf.Pow(square.transform.position.y - pos.y, 2));
            float alpha = Mathf.Max(0, Mathf.Min(1, -.1f * distance + 2.5f));
            square.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        }
    }*/
}
