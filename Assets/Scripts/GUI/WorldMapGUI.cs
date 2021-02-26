using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class WorldMapGUI : GUIDestroyable
    {
        private readonly GameObject[] whiteSquares;
        private readonly GameObject[] blackSquares;
        private readonly GameObject[] features;
        private readonly GameObject gObject;
        private readonly WorldMapHoverController controller;
        private readonly WorldMapRenderController generator;
        private static readonly int SQUARE_SIZE = 5;
        public static bool Activated { get; private set; }
        private static readonly Type[] COMPONENTS = new Type[] { typeof(WorldMapHoverController), typeof(WorldMapRenderController) };

        public WorldMapGUI(IColonizable c)
        {
            Activated = true;

            this.gObject = new GameObject("Map", COMPONENTS);
            controller = gObject.GetComponent<WorldMapHoverController>();
            generator = gObject.GetComponent<WorldMapRenderController>();

            var n = c.Size;
            whiteSquares = new GameObject[n];
            blackSquares = new GameObject[n];
            features = new GameObject[n];

            var rowSize = (int)Math.Ceiling(Math.Pow(n, .5));
            controller.Init(whiteSquares, rowSize);
            generator.Init(this, n, rowSize);
        }

        public static void Render(IColonizable c)
        {
            GUIDestroyable.ClearGUI();
            new WorldMapGUI(c);
        }
        
        public void AddSquare(int idx, int x, int y)
        {
            var loc = new Vector2(SQUARE_SIZE * x, SQUARE_SIZE * y);
            AddBigSquare(idx, loc);
            AddSmallSquare(idx, loc);
            AddLetter(idx, loc);
        }

        private void AddBigSquare(int idx, Vector2 loc)
        {
            var whiteSquare = UnityEngine.Object.Instantiate(Constants.WHITE_SQUARE, loc, Quaternion.identity, Constants.GRID.transform);
            whiteSquares[idx] = whiteSquare;
        }

        private void AddSmallSquare(int idx, Vector2 loc)
        {
            var blackSquare = UnityEngine.Object.Instantiate(Constants.BLACK_SQUARE, loc, Quaternion.identity, Constants.GRID.transform);
            blackSquares[idx] = blackSquare;
        }

        private void AddLetter(int idx, Vector2 loc)
        {
            var letter = UnityEngine.Object.Instantiate(Constants.LETTER, loc, Quaternion.identity, Constants.CANVAS.transform);
            letter.GetComponent<Text>().text = ColonizerR.r.Next(100) < 10 ? ((char)ColonizerR.r.Next('A', 'Z')).ToString() : ""; 
            features[idx] = letter;
        }

        public override void Destroy()
        {
            Activated = false;
            controller.Disabled = true;
            generator.BeginDestruction();
            for (int i = 0; i < whiteSquares.Length; i++)
                DisableSquare(i);
        }

        private void DisableSquare(int i)
        {
            if (whiteSquares[i] == null)
                return;
            whiteSquares[i].SetActive(false);
            blackSquares[i].SetActive(false);
            features[i].SetActive(false);
        }

        public void DestroySquare(int i)
        {
            if(whiteSquares[i] == null)
                return;
            GameObject.Destroy(whiteSquares[i]);
            GameObject.Destroy(blackSquares[i]);
            GameObject.Destroy(features[i]);
            whiteSquares[i] = null;
            blackSquares[i] = null;
            features[i] = null;
        }

        public void FinishDestruction()
        {
            GameObject.Destroy(gObject);
        }
    }
}
