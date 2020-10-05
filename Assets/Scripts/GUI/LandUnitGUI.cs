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
    public class LandUnitGUI : GUIScrollable
    {
        private readonly Vector2 pos;
        private readonly LandUnit unit;
        private static readonly Transform PARENT_TRANSFORM = Constants.MASKING_PANEL.transform;
        private static readonly Font ARIAL = Resources.GetBuiltinResource<Font>("Arial.ttf");
        private readonly IList<GameObject> objects = new List<GameObject>();
        private IList<Transform> transforms = new List<Transform>();
        public LandUnitGUI(LandUnit unit, Vector2 pos)
        {
            this.pos = pos;
            this.unit = unit;
            CreateArableField();
            CreateResourceField();
            CreateStructureField();
            
        }

        private void CreateStructureField()
        {
            GameObject text = new GameObject("Text", typeof(RectTransform));
            var controller = text.AddComponent<StructureLabelController>();
            controller.Unit = unit;
            transforms.Add(text.transform);
            objects.Add(text);
            Text textComponent = text.AddComponent<Text>();
            textComponent.text = unit.Structure == null ? "None" : throw new NotImplementedException();
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
            transform.anchoredPosition = new Vector2(pos.x + 250, pos.y);
            transform.sizeDelta = new Vector2(200, 200);

            var underline = new GameObject("Underline", typeof(RectTransform));
            transforms.Add(underline.transform);
            objects.Add(underline);
            var image = underline.AddComponent<Image>();
            image.color = Color.black;
            var underlineTransform = (RectTransform)underline.transform;
            underlineTransform.SetParent(PARENT_TRANSFORM);
            underlineTransform.anchorMin = new Vector2(0, 1);
            underlineTransform.anchorMax = new Vector2(0, 1);
            underlineTransform.anchoredPosition = new Vector2(pos.x + 300, pos.y - 20);
            underlineTransform.sizeDelta = new Vector2(200, 200);
            underlineTransform.localScale = new Vector3(.5f, .01f, 1);

        }

        private void CreateResourceField()
        {
            GameObject text = new GameObject("Text", typeof(RectTransform));
            transforms.Add(text.transform);
            objects.Add(text);
            Text textComponent = text.AddComponent<Text>();
            textComponent.text = unit.Resource == null ? "None" : Constants.RESOURCE_MAP[(EResource) unit.Resource];
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
            transform.anchoredPosition = new Vector2(pos.x + 100, pos.y);
            transform.sizeDelta = new Vector2(500, 200);
        }

        private void CreateArableField()
        {
            GameObject text = new GameObject("Text", typeof(RectTransform));
            transforms.Add(text.transform);
            objects.Add(text);
            Text textComponent = text.AddComponent<Text>();
            textComponent.text = unit.Arable ? "Yes" : "No";
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
            transform.sizeDelta = new Vector2(200, 200);
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
                UnityEngine.Object.Destroy(gObject);
            }
        }
    }
}
