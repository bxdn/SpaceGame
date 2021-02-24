using Assets.Scripts.Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    class WorldMapController : MonoBehaviour
    {
        public static bool Activated { get; set; }
        private static int MAP_SIZE;
        private static readonly int DISTANCE_ALPHA = 6;
        private static GameObject[] whiteSquares;
        private static readonly GameObject[] prevSquares = new GameObject[(int)Mathf.Pow(DISTANCE_ALPHA * 2, 2)];
        private static int bufferIdx;
        public static void Init(GameObject[] whiteSquares)
        {
            WorldMapController.whiteSquares = whiteSquares;
            for (int i = 0; i < prevSquares.Length; i++)
                prevSquares[i] = null;
            MAP_SIZE = (int) Math.Ceiling(Math.Pow(whiteSquares.Length, .5));
        }
        void Update()
        {
            if (!Activated)
                return;
            var pos = CameraController.Camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            for (int i = 0; i < prevSquares.Length; i++)
                UpdatePrevSquare(prevSquares[i], pos);
            var roundPos = new Vector2((int)(pos.x / 5 + .5), (int)(pos.y / 5 + .5));
            bufferIdx = 0;
            for (int i = -DISTANCE_ALPHA; i < DISTANCE_ALPHA; i++)
                UpdateCurrentSquares(i, roundPos, pos);
        }
        private void UpdateCurrentSquares(int i, Vector2 roundPos, Vector2 pos)
        {
            for (int j = -DISTANCE_ALPHA; j < DISTANCE_ALPHA; j++)
                UpdateSquareRow(i, j, roundPos, pos);
        }
        private void UpdateSquareRow(int i, int j, Vector2 roundPos, Vector2 pos)
        {
            int x = (int)roundPos.x + i;
            int y = (int)roundPos.y + j;
            int idx;
            if (0 <= x && x < MAP_SIZE && 0 <= y && y < MAP_SIZE && 
                (idx = x * MAP_SIZE + y) < whiteSquares.Length)
                UpdateAndBufferSquare(idx, pos);
        }
        private void UpdateAndBufferSquare(int squareIdx, Vector2 pos)
        {
            GameObject square = whiteSquares[squareIdx];
            prevSquares[bufferIdx++] = square;
            UpdateAlpha(square, pos);
        }
        private void UpdatePrevSquare(GameObject prevSquare, Vector2 pos)
        {
            if (prevSquare != null)
                UpdateAlpha(prevSquare, pos);
        }
        private void UpdateAlpha(GameObject square, Vector2 pos)
        {
            float distance = Mathf.Sqrt(Mathf.Pow(square.transform.position.x - pos.x, 2) + Mathf.Pow(square.transform.position.y - pos.y, 2));
            float alpha = Mathf.Max(0, Mathf.Min(1, -.1f * distance + (DISTANCE_ALPHA - 1) / 2.0f));
            square.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        }
    }
}
