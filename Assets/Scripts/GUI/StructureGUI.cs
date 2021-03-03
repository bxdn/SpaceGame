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
    public class StructureGUI : GUIScrollable
    {
        private static int count = 0;
        private static readonly Transform PARENT_TRANSFORM = Constants.STRUCTURE_MASKING_PANEL.transform;
        private static readonly Font ARIAL = Resources.GetBuiltinResource<Font>("Arial.ttf");
        private readonly IList<GameObject> objects = new List<GameObject>();
        private readonly IList<Transform> transforms = new List<Transform>();
        public StructureGUI(EStructure structure)
        {
            count++;
            CreateStructureText(structure);
        }

        private void CreateStructureText(EStructure structure)
        {
            GameObject text = new GameObject("Text", typeof(RectTransform));
            var controller = text.AddComponent<StructureGUIController>();
            controller.Structure = structure;
            transforms.Add(text.transform);
            objects.Add(text);
            Text textComponent = text.AddComponent<Text>();
            textComponent.text = Constants.FEATURE_MAP[structure].Name;
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
            transform.anchoredPosition = new Vector2(10, 25 - 30 * count);
            transform.sizeDelta = new Vector2(600, 60);
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
            count = 0;
        }
    }
}
