using Assets.Scripts.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class LandUnitGUI : GUIScrollable
    {
        private readonly Vector2 pos;
        private static readonly Transform PARENT_TRANSFORM = Constants.MASKING_PANEL.transform;
        private static readonly Font ARIAL = Resources.GetBuiltinResource<Font>("Arial.ttf");
        private readonly IList<GameObject> objects = new List<GameObject>();
        private IList<Transform> transforms = new List<Transform>();
        public LandUnitGUI(StructureInfo structure, int total, Vector2 pos)
        {
            this.pos = pos;
            CreateArableField(structure);
            CreateResourceField(total);            
        }

        private void CreateResourceField(int total)
        {
            GameObject text = new GameObject("Text", typeof(RectTransform));
            transforms.Add(text.transform);
            objects.Add(text);
            Text textComponent = text.AddComponent<Text>();
            textComponent.text = total.ToString();
            textComponent.fontSize = 38;
            textComponent.font = ARIAL;
            textComponent.color = new Color(0, 0, 0, 1);
            textComponent.lineSpacing = 0;
            textComponent.alignment = TextAnchor.UpperLeft;
            RectTransform transform = (RectTransform)text.transform;
            transform.SetParent(PARENT_TRANSFORM);
            transform.anchorMin = new Vector2(0, 1);
            transform.anchorMax = new Vector2(0, 1);
            transform.pivot = new Vector2(0, 1);
            transform.localScale = new Vector3(.5f, .5f, 1);
            transform.anchoredPosition = new Vector2(pos.x + 300, pos.y);
            transform.sizeDelta = new Vector2(500, 200);
        }

        private void CreateArableField(StructureInfo structure)
        {
            GameObject text = new GameObject("Text", typeof(RectTransform));
            transforms.Add(text.transform);
            objects.Add(text);
            Text textComponent = text.AddComponent<Text>();
            textComponent.text = structure.Name;
            textComponent.fontSize = 38;
            textComponent.font = ARIAL;
            textComponent.color = new Color(0, 0, 0, 1);
            textComponent.lineSpacing = 0;
            textComponent.alignment = TextAnchor.UpperLeft;
            RectTransform transform = (RectTransform)text.transform;
            transform.SetParent(PARENT_TRANSFORM);
            transform.anchorMin = new Vector2(0, 1);
            transform.anchorMax = new Vector2(0, 1);
            transform.pivot = new Vector2(0, 1);
            transform.localScale = new Vector3(.5f, .5f, 1);
            transform.anchoredPosition = new Vector2(pos.x + 10, pos.y);
            transform.sizeDelta = new Vector2(500, 200);
        }
        public override void Scroll(bool ascending)
        {
            foreach (Transform transform in transforms)
            {
                transform.Translate(new Vector2(0, ascending ? 25 : -25));
            }
        }
        public override void Destroy()
        {
            foreach(GameObject gObject in objects){
                Object.Destroy(gObject);
            }
        }
    }
}
