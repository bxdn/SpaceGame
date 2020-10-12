using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class GoodServiceGUI : GUIScrollable
    {
        private readonly Vector2 pos;
        private static readonly Transform PARENT_TRANSFORM = Constants.MASKING_PANEL.transform;
        private static readonly Font ARIAL = Resources.GetBuiltinResource<Font>("Arial.ttf");
        private readonly IList<GameObject> objects = new List<GameObject>();
        private IList<Transform> transforms = new List<Transform>();
        public GoodServiceGUI(EGood good, GoodInfo value, Vector2 pos)
        {
            this.pos = pos;
            CreateNameField(Constants.GOOD_MAP[good]);
            CreateNumberField(value.Value);
            CreateDirectionField(value.Increasing);
        }
        public GoodServiceGUI(EService service, float value, Vector2 pos)
        {
            this.pos = pos;
            CreateNameField(Constants.SERVICE_MAP[service]);
            CreateNumberField(value);
        }
        public GoodServiceGUI(EResource resource, int value, Vector2 pos)
        {
            this.pos = pos;
            CreateNameField(Constants.RESOURCE_MAP[resource]);
            CreateNumberField(value);
        }
        private void CreateDirectionField(bool increasing)
        {
            GameObject text = new GameObject("Text", typeof(RectTransform));
            transforms.Add(text.transform);
            objects.Add(text);
            Text textComponent = text.AddComponent<Text>();
            textComponent.text = increasing ? '\u2191'.ToString() : '\u2193'.ToString();
            textComponent.fontSize = 38;
            textComponent.font = ARIAL;
            textComponent.color = new Color(0, 0, 0, 1);
            textComponent.lineSpacing = 0;
            textComponent.fontStyle = FontStyle.Bold;
            textComponent.alignment = TextAnchor.UpperLeft;
            RectTransform transform = (RectTransform)text.transform;
            transform.SetParent(PARENT_TRANSFORM);
            transform.anchorMin = new Vector2(0, 1);
            transform.anchorMax = new Vector2(0, 1);
            transform.pivot = new Vector2(0, 1);
            transform.localScale = new Vector3(.5f, .5f, 1);
            transform.anchoredPosition = new Vector2(pos.x + 850, pos.y);
            transform.sizeDelta = new Vector2(50, 50);
        }
        private void CreateNumberField(float value)
        {
            GameObject text = new GameObject("Text", typeof(RectTransform));
            transforms.Add(text.transform);
            objects.Add(text);
            Text textComponent = text.AddComponent<Text>();
            textComponent.text = value.ToString();
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
            transform.anchoredPosition = new Vector2(pos.x + 775, pos.y);
            transform.sizeDelta = new Vector2(500, 200);
        }

        public override void Scroll(bool ascending)
        {
            /*foreach(Transform transform in transforms)
            {
                transform.Translate(new Vector2(0, ascending ? 25 : -25));
            }*/
        }
        private void CreateNameField(string name)
        {
            GameObject text = new GameObject("Text", typeof(RectTransform));
            transforms.Add(text.transform);
            objects.Add(text);
            Text textComponent = text.AddComponent<Text>();
            textComponent.text = name;
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
            transform.anchoredPosition = new Vector2(pos.x + 600, pos.y);
            transform.sizeDelta = new Vector2(400, 200);
        }
        public override void Destroy()
        {
            foreach(GameObject gObject in objects){
                UnityEngine.Object.Destroy(gObject);
            }
        }
    }
}
