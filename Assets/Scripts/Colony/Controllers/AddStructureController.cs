using Assets.Scripts.Model;
using Assets.Scripts.Registry;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class AddStructureController : MonoBehaviour
    {
        private static bool activated;
        private static IColonizableManager manager;
        private static StructureInfo structure;
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
        public static void Activate(StructureInfo structure, IColonizableManager manager)
        {
            lastClicked = -1;
            activated = true;
            AddStructureController.structure = structure;
            AddStructureController.manager = manager;
            rowSize = Utils.GetRowSize(manager.Size);
            Constants.MENUS_BUTTON.SetActive(false);
            Constants.COLONY_BUTTON.SetActive(false);
            Constants.COLONIZE_BUTTON.SetActive(false);
            Constants.RESET_BUTTON.SetActive(false);
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
            if (structure == RegistryUtil.Structures.GetStructure("HQ") ||
                manager.CurrentColony.CanBuildStructure(structure))
                SetBuildableStructure();
        }
        private static void SetBuildableStructure()
        {
            var squareIdx = Utils.GetCurrentSquareIdx(rowSize);
            var requiredSquareFeature = RegistryUtil.Resources.Get(structure.PrereqFeature);
            var actualSquareFeature = manager.GetFeature(squareIdx);
            var servicePlaceable = manager.CurrentColony != null && manager.CurrentColony.IsServicePlaceable(squareIdx);
            var industryPlaceable = manager.CurrentColony != null && manager.CurrentColony.IsIndustryPlaceable(squareIdx);
            var isService = !(structure).ServiceFlow.IsEmpty;
            var hq = RegistryUtil.Structures.GetStructure("HQ");
            var structureIsPlacable = requiredSquareFeature.Equals(actualSquareFeature) && 
                (structure == hq || !isService && industryPlaceable || servicePlaceable);
            if (structureIsPlacable)
                SetStructure(squareIdx);
        }
        private static void SetStructure(int idx)
        {
            var hq = RegistryUtil.Structures.GetStructure("HQ");
            if (structure != hq)
                AddStandardStructure(idx);
            else
                AddHQ(idx);
        }
        private static void AddStandardStructure(int idx)
        {
            manager.CurrentColony.AddStructure(structure, idx);
            UpdateGUIS(idx);
        }
        private static void AddHQ(int idx)
        {
            var firstCol = WorldGeneration.Galaxy.Player.Colonies.Count == 0;
            manager.Colonize(idx);
            if (firstCol)
                GiveFirstColonyResources();
            WorldMapRenderController.CreateColonySquare(idx);
            WorldMapRenderController.ShowBuildableSquares();
            UpdateGUIS(idx);
            RevertView();
        }
        private static void GiveFirstColonyResources()
        {
            var col = manager.CurrentColony;
            col.IncrementGood(RegistryUtil.GoodsServices.Get("Chips"), 100);
            col.IncrementGood(RegistryUtil.GoodsServices.Get("Energy"), 100);
            col.IncrementGood(RegistryUtil.GoodsServices.Get("Steel"), 100);
        }
        private static void UpdateGUIS(int idx)
        {
            manager.UpdateFeature(idx, structure);
            WorldMapRenderController.ModifySquare(idx);
            var col = manager.CurrentColony;
            ColonyDialogController.Reset(col);
            GoodsDialogController.Reset(col);
        }
        private static void RevertView()
        {
            if (manager.CurrentColony != null)
                RevertToColonyView();
            Constants.COLONY_BUTTON.SetActive(true);
            Constants.COLONIZE_PROMPT.SetActive(false);
            Constants.COLONIZE_BUTTON.SetActive(true);
            Constants.RESET_BUTTON.SetActive(true);
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
