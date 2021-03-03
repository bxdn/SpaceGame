using Assets.Scripts.GUI;
using System;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class WorldMapHoverController : MonoBehaviour
    {
        public bool Disabled { get; set; } = false;
        private int rowSize;
        private int n;
        private static readonly int DISTANCE_ALPHA = 6;
        private static readonly int[] prevSquares = new int[(int)Mathf.Pow(DISTANCE_ALPHA * 2, 2)];
        private static int prevIdx;
        private WorldMapGUI gui;
        public void Init(int n, WorldMapGUI gui)
        {
            this.n = n;
            rowSize = Utils.GetRowSize(n);
            this.gui = gui;
        }
        private void Update()
        {
            if (!Disabled)
                MoveSpotlight();
        }

        private void MoveSpotlight()
        {
            var mousePos = Utils.GetCurrentMousePosition();
            for (int i = 0; i < prevSquares.Length; i++)
                UpdateAlpha(prevSquares[i], mousePos);
            var squareCoords = Utils.GetCurrentSquareCoords();
            prevIdx = 0;
            for (int i = -DISTANCE_ALPHA; i < DISTANCE_ALPHA; i++)
                UpdateCurrentSquaresAlpha(i, squareCoords, mousePos);
        }

        private void UpdateCurrentSquaresAlpha(int i, Vector2Int squareCoords, Vector2 mousePos)
        {
            for (int j = -DISTANCE_ALPHA; j < DISTANCE_ALPHA; j++)
                CheckAndUpdateSquareAlpha(i, j, squareCoords, mousePos);
        }
        private void CheckAndUpdateSquareAlpha(int i, int j, Vector2Int roundedMousePos, Vector2 mousePos)
        {
            int x = roundedMousePos.x + i;
            int y = roundedMousePos.y + j;
            int idx;
            if (0 <= x && x < rowSize && 0 <= y && y < rowSize && 
                (idx = Utils.SquareCoordsToIdx(new Vector2Int(x,y),rowSize)) < n)
                UpdateAndBufferSquareAlpha(idx, mousePos);
        }
        private void UpdateAndBufferSquareAlpha(int squareIdx, Vector2 mousePos)
        {
            prevSquares[prevIdx++] = squareIdx;
            UpdateAlpha(squareIdx, mousePos);
        }
        private void UpdateAlpha(int squareIdx, Vector2 mousePos)
        {
            var coords = Utils.SquareIdxToWorldCoords(squareIdx, rowSize);
            var distance = Mathf.Sqrt(Mathf.Pow(coords.x - mousePos.x, 2) 
                + Mathf.Pow(coords.y - mousePos.y, 2));
            var alpha = Mathf.Max(0, Mathf.Min(1, -.1f * distance + (DISTANCE_ALPHA - 1) / 2.0f));
            gui.SetWhiteSquareAlpha(squareIdx, alpha);
        }
    }
}
