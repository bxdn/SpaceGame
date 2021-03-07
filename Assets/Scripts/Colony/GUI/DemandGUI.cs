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
    public class DemandGUI : GUIScrollable
    {
        private readonly Vector2 pos;
        private static readonly Transform PARENT_TRANSFORM = Constants.GOOD_MASKING_PANEL.transform;
        private static readonly Font ARIAL = Resources.GetBuiltinResource<Font>("Arial.ttf");
        private readonly IList<GameObject> objects = new List<GameObject>();
        private IList<Transform> transforms = new List<Transform>();
        public DemandGUI(EGood good, float value, Vector2 pos)
        {
            this.pos = pos;
            CreateNameField(Constants.GOOD_MAP[good]);
            CreateNumberField(value);
        }
        public DemandGUI(EService service, float value, Vector2 pos)
        {
            this.pos = pos;
            CreateNameField(Constants.SERVICE_MAP[service]);
            CreateNumberField(value);
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
            transform.anchoredPosition = new Vector2(pos.x, pos.y);
            transform.sizeDelta = new Vector2(1000, 200);
        }

        public override void Scroll(bool ascending)
        {
            foreach (Transform transform in transforms)
            {
                transform.Translate(new Vector2(0, ascending ? 25 : -25));
            }
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
            transform.anchoredPosition = new Vector2(pos.x + 50, pos.y);
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
