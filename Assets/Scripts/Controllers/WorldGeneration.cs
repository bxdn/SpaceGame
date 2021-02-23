using Assets.Scripts.Model;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public static Galaxy Galaxy { get; private set; }
    private static readonly int MAP_SIZE = 100;
    private static readonly int DISTANCE_ALPHA = 6;
    private GameObject[] whiteSquares = new GameObject[MAP_SIZE * MAP_SIZE];
    private GameObject[] prevSquares = new GameObject[(int)Mathf.Pow(DISTANCE_ALPHA*2, 2)];
    void Awake()
    {
        Application.targetFrameRate = 60;
        //StartWorld();
        int idx = 0;
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                Vector2 loc = new Vector2(5 * i, 5 * j);
                GameObject whiteSquare = new GameObject("Square");
                whiteSquares[idx++] = whiteSquare;
                whiteSquare.transform.parent = Constants.GRID.transform;
                var renderer = whiteSquare.AddComponent<SpriteRenderer>();
                renderer.sprite = Constants.square;
                renderer.color = new Color(1,1,1,0);
                whiteSquare.transform.position = loc;
                whiteSquare.transform.localScale = new Vector2(5, 5);
                GameObject blackSquare = new GameObject("Square");
                blackSquare.transform.parent = Constants.GRID.transform;
                var smallRenderer = blackSquare.AddComponent<SpriteRenderer>();
                smallRenderer.sprite = Constants.square;
                var rand = ColonizerR.r.Next(100);
                smallRenderer.color = rand < 10 ? Color.white : Color.black;
                smallRenderer.sortingOrder = 1;
                blackSquare.transform.position = loc;
                blackSquare.transform.localScale = new Vector2(4.7f, 4.7f);
            }
        }
    }
    public static void StartWorld()
    {
        Debug.Log("Starting Galaxy Generation!");
        Galaxy = Save.SavedGameFound() ? Save.LoadGame() : new Galaxy();
        Debug.Log("Finished Galaxy Generation!");
        Galaxy.RenderStartingSystem();
    }
    void Update()
    {
        var pos = CameraController.Camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        for (int i = 0; i < prevSquares.Length; i++)
        {
            var prevSquare = prevSquares[i];
            if(prevSquare != null)
                UpdateAlpha(prevSquare, pos);
        }
        var roundPos = new Vector2((int) (pos.x / 5 + .5) , (int) (pos.y / 5 + .5));
        for (int i = -DISTANCE_ALPHA, idx = 0; i < DISTANCE_ALPHA; i++)
        {
            for (int j = -DISTANCE_ALPHA; j < DISTANCE_ALPHA; j++)
            {
                int x = (int)roundPos.x + i;
                int y = (int)roundPos.y + j;
                if (0 <= x && x < MAP_SIZE && 0 <= y && y < MAP_SIZE)
                {
                    GameObject square = whiteSquares[x * MAP_SIZE + y];
                    prevSquares[idx++] = square;
                    UpdateAlpha(square, pos);
                }
            }
        }
    }
    private void UpdateAlpha(GameObject square, Vector2 pos)
    {
        float distance = Mathf.Sqrt(Mathf.Pow(square.transform.position.x - pos.x, 2) + Mathf.Pow(square.transform.position.y - pos.y, 2));
        float alpha = Mathf.Max(0, Mathf.Min(1, -.1f * distance + (DISTANCE_ALPHA-1)/2.0f));
        square.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
    }
}
