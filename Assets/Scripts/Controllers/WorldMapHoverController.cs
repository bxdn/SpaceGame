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
        private static int bufferIdx;
        private WorldMapGUI gui;
        public void Init(int n, WorldMapGUI gui)
        {
            this.n = n;
            rowSize = (int)Math.Ceiling(Math.Pow(n, .5));
            this.gui = gui;
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
                UpdateAlpha(prevSquares[i], mousePos);
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
                (idx = x * rowSize + y) < n)
                UpdateAndBufferSquareAlpha(idx, mousePos);
        }
        private void UpdateAndBufferSquareAlpha(int squareIdx, Vector2 mousePos)
        {
            prevSquares[bufferIdx++] = squareIdx;
            UpdateAlpha(squareIdx, mousePos);
        }
        private void UpdateAlpha(int squareIdx, Vector2 mousePos)
        {
            var distance = Mathf.Sqrt(Mathf.Pow((squareIdx  % rowSize) * WorldMapGUI.SQUARE_SIZE  - mousePos.x, 2) 
                + Mathf.Pow((squareIdx / rowSize) * WorldMapGUI.SQUARE_SIZE - mousePos.y, 2));
            var alpha = Mathf.Max(0, Mathf.Min(1, -.1f * distance + (DISTANCE_ALPHA - 1) / 2.0f));
            gui.SetWhiteSquareAlpha(squareIdx, alpha);
        }
    }
}
