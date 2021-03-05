using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class AddStructureController : MonoBehaviour
    {
        private static bool activated;
        private static IColonizableManager manager;
        private static EStructure structure;
        private static int rowSize;
        private static float lastClicked = -1;

        public void Update()
        {
            if (!activated)
                return;
            if (Input.GetMouseButtonDown(0))
                TriggerClicked();
            else if (Input.GetMouseButtonDown(1))
                RevertView();
        }
        public static void Activate(EStructure structure, IColonizableManager manager)
        {
            lastClicked = -1;
            activated = true;
            AddStructureController.structure = structure;
            AddStructureController.manager = manager;
            rowSize = Utils.GetRowSize(manager.Size);
            Constants.MENUS_BUTTON.SetActive(false);
            Constants.COLONY_BUTTON.SetActive(false);
        }
        private static void TriggerClicked()
        {
            var newTime = Time.time;
            if (newTime - lastClicked <= .3)
                TriggerDoubleClicked();
            lastClicked = newTime;
        }
        private static void TriggerDoubleClicked()
        {
            var colonyCanBuildStructure = structure == EStructure.LogisticsStation || 
                StructureGUIController.ValidateColony(manager.CurrentColony, structure);
            if (colonyCanBuildStructure)
                SetBuildableStructure();
        }
        private static void SetBuildableStructure()
        {
            var squareIdx = Utils.GetCurrentSquareIdx(rowSize);
            var requiredSquareFeature = ((StructureInfo) Constants.FEATURE_MAP[structure]).PrereqFeature;
            var actualSquareFeature = manager.GetFeature(squareIdx);
            var servicePlaceable = manager.CurrentColony != null && manager.CurrentColony.IsServicePlaceable(squareIdx);
            var isService = !(Constants.FEATURE_MAP[structure] as StructureInfo).ServiceFlow.IsEmpty;
            var structureIsPlacable = requiredSquareFeature.Equals(actualSquareFeature) && 
                (!isService || servicePlaceable);
            if (structureIsPlacable)
                SetStructure(squareIdx);
        }
        private static void SetStructure(int idx)
        {
            if (structure != EStructure.LogisticsStation)
                AddStandardStructure(manager.CurrentColony, idx);
            else
                AddLogisticsStation(idx);
        }
        private static void AddStandardStructure(Colony col, int idx)
        {
            col.AddStructure(structure, idx);
            UpdateGUIS(col, idx);
        }
        private static void AddLogisticsStation(int idx)
        {
            if (manager.CurrentColony == null)
                manager.Colonize();
            var col = manager.CurrentColony;
            col.AddLogisticStructure(idx);
            UpdateGUIS(col, idx);
            RevertView();
        }
        private static void UpdateGUIS(Colony col, int idx)
        {
            manager.UpdateFeature(idx, structure);
            WorldMapRenderController.RenderSquare(idx);
            ColonyDialogController.Reset(col);
            GoodsDialogController.Reset(col);
        }
        private static void RevertView()
        {
            if (manager.CurrentColony != null)
                RevertToColonyView();
            else
                Constants.COLONIZE_BUTTON.SetActive(true);
            Constants.COLONY_BUTTON.SetActive(true);
            Constants.COLONIZE_PROMPT.SetActive(false);
            activated = false;
        }
        private static void RevertToColonyView()
        {
            Constants.MENUS_BUTTON.SetActive(true);
            Constants.COLONY_PANEL.SetActive(true);
            Constants.GOODS_PANEL.SetActive(true);
            CameraController.Locked = true;
        }
    }
}
