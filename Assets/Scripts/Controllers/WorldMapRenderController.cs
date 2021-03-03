using Assets.Scripts.GUI;
using System;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class WorldMapRenderController : MonoBehaviour
    {
        public static WorldMapRenderController latestInstance;
        private bool destroying = false;
        private int curIdx = 0;
        private int totalSquares;
        private int rowSize;
        private IColonizableManager c;
        private static readonly int SQUARES_PER_FRAME = 50;

        private WorldMapGUI gui;

        public void Init(int totalSquares, WorldMapGUI gui, IColonizableManager c )
        {
            this.totalSquares = totalSquares;
            rowSize = Utils.GetRowSize(totalSquares);
            this.gui = gui;
            this.c = c;
            latestInstance = this;
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
                gui.AddSquare(curIdx, Utils.SquareIdxToWorldCoords(curIdx, rowSize), ChooseString(curIdx++));
        }

        public static void RenderSquare(int idx)
        {
            latestInstance.gui.ChangeField(idx, latestInstance.ChooseString(idx));
        }

        private void DestroySquareBatch()
        {
            for (int i = 0; i < SQUARES_PER_FRAME && curIdx < totalSquares; i++)
                gui.DestroySquare(curIdx++);
        }
        private string ChooseString(int idx)
        {
            var field = c.GetFeature(idx);
            if (field == null)
                return "X";
            return Constants.FEATURE_MAP[field].Code;
        }
    }
}
