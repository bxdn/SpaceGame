using Assets.Scripts.GUI;
using Assets.Scripts.Model;
using System;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class WorldMapRenderController : MonoBehaviour
    {
        public static WorldMapRenderController latestInstance;
        private bool destroying = false;
        private bool doneRendering = false;
        private int curIdx = 0;
        private int totalSquares;
        private int rowSize;
        private IColonizableManager manager;
        private static readonly int SQUARES_PER_FRAME = 50;

        private WorldMapGUI gui;

        public void Init(int totalSquares, WorldMapGUI gui, IColonizableManager c )
        {
            this.totalSquares = totalSquares;
            rowSize = Utils.GetRowSize(totalSquares);
            this.gui = gui;
            this.manager = c;
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
            if(curIdx >= totalSquares && !doneRendering)
            {
                ShowBuildableSquares();
                doneRendering = true;
            }
            for (int i = 0; i < SQUARES_PER_FRAME && curIdx < totalSquares; i++)
            {
                gui.AddSquare(curIdx, Utils.SquareIdxToWorldCoords(curIdx, rowSize), ChooseString(curIdx));
                var feature = manager.GetFeature(curIdx);
                if (feature != null && feature.Equals(EStructure.HQ))
                    CreateColonySquare(curIdx);
                curIdx++;
            }
        }

        public static void ShowBuildableSquares()
        {
            var manager = latestInstance.manager;
            for (int i = 0; i < manager.Size; i++)
                ChangeColor(i, new Color(1, 1, 1, 0));
            for (int i = 0; i < manager.Size; i++)
                if (manager.CurrentColony is Colony c && c.IsIndustryPlaceable(i))
                    ChangeColor(i, new Color(.5f, .7f, 1));
            for (int i = 0; i < manager.Size; i++)
                if (manager.CurrentColony is Colony c && c.IsServicePlaceable(i))
                    ChangeColor(i, new Color(.5f, 1, .5f));
        }

        private static void ChangeColor(int idx, Color color)
        {
            latestInstance.gui.ChangeColor(idx, color);
        }

        public static void ModifySquare(int idx)
        {
            latestInstance.gui.ChangeField(idx, latestInstance.ChooseString(idx));
        }

        public static void CreateColonySquare(int idx)
        {
            latestInstance.gui.CreateColonyController(idx);
        }

        public static void UpdateWhiteSquareAlpha(int idx, float a)
        {
            if (!(latestInstance.manager.CurrentColony is Colony c) || !c.IsIndustryPlaceable(idx))
                latestInstance.gui.SetWhiteSquareAlpha(idx, a);
        }

        private void DestroySquareBatch()
        {
            for (int i = 0; i < SQUARES_PER_FRAME && curIdx < totalSquares; i++)
                gui.DestroySquare(curIdx++);
        }
        private string ChooseString(int idx)
        {
            var field = manager.GetFeature(idx);
            if (field == null)
                return "X";
            return Constants.FEATURE_MAP[field].Code;
        }
    }
}
