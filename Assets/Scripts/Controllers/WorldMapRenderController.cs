using Assets.Scripts.GUI;
using System;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class WorldMapRenderController : MonoBehaviour
    {
        private bool destroying = false;
        private int curIdx = 0;
        private int totalSquares;
        private int rowSize;
        private static readonly int SQUARES_PER_FRAME = 50;

        private WorldMapGUI gui;

        public void Init(int totalSquares, WorldMapGUI gui )
        {
            this.totalSquares = totalSquares;
            rowSize = (int)Math.Ceiling(Math.Pow(totalSquares, .5));
            this.gui = gui;
        }

        private void Update()
        {
            if (destroying)
                Destroy();
            else
                RenderSquareBatch();
        }

        private void Destroy()
        {
            if (curIdx < totalSquares)
                DestroySquareBatch();
            else
                gui.FinishDestruction();
        }

        public void BeginDestruction()
        {
            destroying = true;
            curIdx = 0;
        }
        private void RenderSquareBatch()
        {
            for (int i = 0; i < SQUARES_PER_FRAME && curIdx < totalSquares; i++)
                gui.AddSquare(curIdx, (curIdx % rowSize) * WorldMapGUI.SQUARE_SIZE, 
                    (curIdx++ / rowSize) * WorldMapGUI.SQUARE_SIZE);
        }
        private void DestroySquareBatch()
        {
            for (int i = 0; i < SQUARES_PER_FRAME && curIdx < totalSquares; i++)
                gui.DestroySquare(curIdx++);
        }
    }
}
