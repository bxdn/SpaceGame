using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class WorldMapGUI : GUIDestroyable
    {
        private readonly Square[] squares;
        private readonly GameObject gObject;
        private readonly WorldMapHoverController hoverController;
        private readonly WorldMapRenderController renderController;
        public static readonly int SQUARE_SIZE = 5;
        public static bool Activated { get; private set; }
        private static readonly Type[] COMPONENTS = new Type[] { typeof(WorldMapHoverController), typeof(WorldMapRenderController) };

        public WorldMapGUI(IColonizable c)
        {
            Activated = true;

            gObject = new GameObject("Map", COMPONENTS);
            hoverController = gObject.GetComponent<WorldMapHoverController>();
            renderController = gObject.GetComponent<WorldMapRenderController>();

            var n = c.Size;
            squares = new Square[n];

            hoverController.Init(n, this);
            renderController.Init(n, this);
        }

        public static void Render(IColonizable c)
        {
            GUIDestroyable.ClearGUI();
            new WorldMapGUI(c);
        }

        public void SetWhiteSquareAlpha(int idx, float alpha)
        {
            var square = squares[idx];
            if(square != null)
                square.WhiteSquare.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        }

        public void AddSquare(int idx, int x, int y)
        {
            var loc = new Vector2(x, y);
            var square = new Square(AddBigSquare(loc), AddSmallSquare(loc), AddLetter(loc));
            squares[idx] = square;
        }

        private GameObject AddBigSquare(Vector2 loc)
        {
            return UnityEngine.Object.Instantiate(Constants.WHITE_SQUARE, loc, 
                Quaternion.identity, Constants.GRID.transform);
        }

        private GameObject AddSmallSquare(Vector2 loc)
        {
            return UnityEngine.Object.Instantiate(Constants.BLACK_SQUARE, loc,
                Quaternion.identity, Constants.GRID.transform);
        }

        private GameObject AddLetter(Vector2 loc)
        {
            var letter = UnityEngine.Object.Instantiate(Constants.LETTER, loc, 
                Quaternion.identity, Constants.CANVAS.transform);
            letter.GetComponent<Text>().text = ColonizerR.r.Next(100) < 10 ? 
                ((char)ColonizerR.r.Next('A', 'Z')).ToString() : "";
            return letter;
        }

        public override void Destroy()
        {
            Activated = false;
            hoverController.Disabled = true;
            renderController.BeginDestruction();
            for (int i = 0; i < squares.Length; i++)
                DisableSquare(i);
        }

        private void DisableSquare(int i)
        {
            var square = squares[i];
            if (square == null)
                return;
            square.WhiteSquare.SetActive(false);
            square.BlackSquare.SetActive(false);
            square.Feature.SetActive(false);
        }

        public void DestroySquare(int i)
        {
            var square = squares[i];
            if (square == null)
                return;
            GameObject.Destroy(square.WhiteSquare);
            GameObject.Destroy(square.BlackSquare);
            GameObject.Destroy(square.Feature);
            squares[i] = null;
        }

        public void FinishDestruction()
        {
            GameObject.Destroy(gObject);
        }

        private class Square
        {
            public GameObject BlackSquare { get; }
            public GameObject WhiteSquare { get; }
            public GameObject Feature { get; }
            public Square(GameObject whiteSquare, GameObject blackSquare, GameObject feature)
            {
                BlackSquare = blackSquare;
                WhiteSquare = whiteSquare;
                Feature = feature;
            }
        }
    }
}
