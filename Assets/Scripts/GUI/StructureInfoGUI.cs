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
    public class StructureInfoGUI : GUIScrollable
    {
        private readonly StructureInfo info;
        private static readonly Transform PARENT_TRANSFORM = Constants.STRUCTURE_MASKING_PANEL.transform;
        private static readonly Font ARIAL = Resources.GetBuiltinResource<Font>("Arial.ttf");
        private readonly IList<GameObject> objects = new List<GameObject>();
        private readonly IList<Transform> transforms = new List<Transform>();
        public StructureInfoGUI(StructureInfo info)
        {
            this.info = info;
            CreatePriceText();
            CreateGoodFlowText();
            CreateServiceFlowText();
        }

        private void CreatePriceText()
        {
            int count = 1;
            CreateElement("Workers: " + info.RequiredWorkers + " (Lvl " + info.WorkerLevel + "+)", new Vector2(260, 25 - 30 * count));
            foreach(KeyValuePair<Model.EGood, int> entry in info.GoodCost)
            {
                count++;
                CreateElement(Constants.GOOD_MAP[entry.Key] + ": " + entry.Value, new Vector2(260, 25 - 30 * count));
            }
        }
        private void CreateElement(string name, Vector2 pos)
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
            transform.anchoredPosition = pos;
            transform.sizeDelta = new Vector2(400, 200);
        }
        private void CreateGoodFlowText()
        {
            int count = 0;
            foreach (KeyValuePair<Model.EGood, float> entry in info.Flow)
            {
                count++;
                CreateElement(Constants.GOOD_MAP[entry.Key] + ": " + entry.Value, 
                    new Vector2(475, 25 - 30 * count));
            }
        }
        private void CreateServiceFlowText()
        {
            int count = 0;
            foreach (KeyValuePair<Model.EService, float> entry in info.ServiceFlow)
            {
                count++;
                CreateElement(Constants.SERVICE_MAP[entry.Key] + ": " + entry.Value,
                    new Vector2(680, 25 - 30 * count));
            }
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
