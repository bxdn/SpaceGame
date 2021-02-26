using Assets.Scripts.GUI;
using System;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class WorldMapHoverController : MonoBehaviour
    {
        public bool Disabled { get; set; } = false;
        private int rowSize;
        private GameObject[] whiteSquares;
        private static readonly int DISTANCE_ALPHA = 6;
        private static readonly GameObject[] prevSquares = new GameObject[(int)Mathf.Pow(DISTANCE_ALPHA * 2, 2)];
        private static int bufferIdx;
        public void Init(GameObject[] whiteSquares, int rowSize)
        {
            this.whiteSquares = whiteSquares;
            this.rowSize = rowSize;
        }
        private void Update()
        {
            if (!Disabled)
                MoveSpotlight();
        }

        private void MoveSpotlight()
        {
            var mousePos = CameraController.Camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            for (int i = 0; i < prevSquares.Length; i++)
                UpdatePrevSquareAlpha(prevSquares[i], mousePos);
            var roundedMousePos = new Vector2((int)(mousePos.x / 5 + .5), (int)(mousePos.y / 5 + .5));
            bufferIdx = 0;
            for (int i = -DISTANCE_ALPHA; i < DISTANCE_ALPHA; i++)
                UpdateCurrentSquaresAlpha(i, roundedMousePos, mousePos);
        }

        private void UpdateCurrentSquaresAlpha(int i, Vector2 roundPos, Vector2 pos)
        {
            for (int j = -DISTANCE_ALPHA; j < DISTANCE_ALPHA; j++)
                CheckAndUpdateSquareAlpha(i, j, roundPos, pos);
        }
        private void CheckAndUpdateSquareAlpha(int i, int j, Vector2 roundedMousePos, Vector2 mousePos)
        {
            int x = (int)roundedMousePos.y + i;
            int y = (int)roundedMousePos.x + j;
            int idx;
            if (0 <= x && x < rowSize && 0 <= y && y < rowSize && 
                (idx = x * rowSize + y) < whiteSquares.Length)
                UpdateAndBufferSquareAlpha(idx, mousePos);
        }
        private void UpdateAndBufferSquareAlpha(int squareIdx, Vector2 mousePos)
        {
            var square = whiteSquares[squareIdx];
            prevSquares[bufferIdx++] = square;
            if(square != null)
                UpdateAlpha(square, mousePos);
        }
        private static void UpdatePrevSquareAlpha(GameObject prevSquare, Vector2 mousePos)
        {
            if (prevSquare != null)
                UpdateAlpha(prevSquare, mousePos);
        }
        private static void UpdateAlpha(GameObject square, Vector2 mousePos)
        {
            var distance = Mathf.Sqrt(Mathf.Pow(square.transform.position.x - mousePos.x, 2) + Mathf.Pow(square.transform.position.y - mousePos.y, 2));
            var alpha = Mathf.Max(0, Mathf.Min(1, -.1f * distance + (DISTANCE_ALPHA - 1) / 2.0f));
            square.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        }
    }
}
