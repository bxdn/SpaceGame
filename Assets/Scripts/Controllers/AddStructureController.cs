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
            var colonyCanBuildStructure = StructureGUIController.ValidateColony(manager.Colony, structure);
            if (colonyCanBuildStructure)
                SetBuildableStructure();
        }
        private static void SetBuildableStructure()
        {
            var squareIdx = Utils.GetCurrentSquareIdx(rowSize);
            var requiredSquareFeature = Constants.STRUCTURE_MAP[structure].PrereqFeature;
            var actualSquareFeature = manager.GetFeature(squareIdx);
            var structureIsPlacable = requiredSquareFeature.Equals(actualSquareFeature);
            if (structureIsPlacable)
                SetStructure(squareIdx);
        }
        private static void SetStructure(int idx)
        {
            var col = manager.Colony;
            col.AddStructure(structure);
            manager.UpdateFeature(idx, structure);
            WorldMapRenderController.RenderSquare(idx);
            ColonyDialogController.Reset(col);
            GoodsDialogController.Update(col);
        }
        private static void RevertView()
        {
            Constants.MENUS_BUTTON.SetActive(true);
            Constants.COLONY_BUTTON.SetActive(true);
            Constants.COLONY_PANEL.SetActive(true);
            Constants.GOODS_PANEL.SetActive(true);
            activated = false;
        }
    }
}
