using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    class WorldMapGUI : GUIDestroyable
    {
        private static GameObject[] whiteSquares;
        private static GameObject[] blackSquares;
        private static GameObject[] features;

        public WorldMapGUI(IColonizable c)
        {
            int n = c.Size;
            whiteSquares = new GameObject[n];
            blackSquares = new GameObject[n];
            features = new GameObject[n];
            int rowSize = (int)Math.Ceiling(Math.Pow(whiteSquares.Length, .5));
            CameraController.Reset();
            Font ARIAL = Resources.GetBuiltinResource<Font>("Arial.ttf");
            int idx = 0;
            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < rowSize && idx < whiteSquares.Length; j++)
                {
                    Vector2 loc = new Vector2(5 * i, 5 * j);
                    GameObject whiteSquare = new GameObject("Square");
                    whiteSquares[idx] = whiteSquare;
                    whiteSquare.transform.parent = Constants.GRID.transform;
                    var renderer = whiteSquare.AddComponent<SpriteRenderer>();
                    renderer.sprite = Constants.square;
                    renderer.color = new Color(1, 1, 1, 0);
                    whiteSquare.transform.position = loc;
                    whiteSquare.transform.localScale = new Vector2(5, 5);

                    GameObject blackSquare = new GameObject("Square");
                    blackSquares[idx] = blackSquare;
                    blackSquare.transform.parent = Constants.GRID.transform;
                    var smallRenderer = blackSquare.AddComponent<SpriteRenderer>();
                    smallRenderer.sprite = Constants.square;
                    smallRenderer.color = Color.black;
                    smallRenderer.sortingOrder = 1;
                    blackSquare.transform.position = loc;
                    blackSquare.transform.localScale = new Vector2(4.7f, 4.7f);

                    GameObject letter = new GameObject("Letter", typeof(RectTransform));
                    features[idx++] = letter;
                    letter.transform.parent = Constants.CANVAS.transform;
                    letter.transform.localScale = new Vector2(.05f, .05f);
                    var text = letter.AddComponent<Text>();
                    text.alignment = TextAnchor.MiddleCenter;
                    text.text = ColonizerR.r.Next(100) < 10 ? ((char)ColonizerR.r.Next('A', 'Z')).ToString() : "";
                    text.font = ARIAL;
                    text.fontSize = 72;
                    text.color = Color.white;
                    letter.transform.position = loc;
                }
            }
            WorldMapController.Init(whiteSquares);
        }

        public override void Destroy()
        {
            for (int i = 0; i < whiteSquares.Length; i++)
                DestroySquare(i);
        }

        private void DestroySquare(int i)
        {
            GameObject.Destroy(whiteSquares[i]);
            GameObject.Destroy(blackSquares[i]);
            GameObject.Destroy(features[i]);
        }
    }
}
