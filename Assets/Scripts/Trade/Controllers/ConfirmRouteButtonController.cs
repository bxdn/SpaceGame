using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using Assets.Scripts.Trade.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    class ConfirmRouteButtonController : MonoBehaviour, IPointerClickHandler
    {
        private InputField colonyNameField;
        private InputField sentGoodField;
        private InputField receivedGoodField;
        private InputField sentAmountField;
        private InputField receivedAmountField;

        private float sentAmount;
        private float receivedAmount;
        private EGood? sentGood;
        private EGood? receivedGood;
        public void OnPointerClick(PointerEventData eventData)
        {

            if (!ValidateSentGood() || !ValidateReceivedGood())
                return;

            var otherColony = FindOtherColony();
            if (otherColony == null)
                return;

            var manager = (Selection.CurrentSelection as IColonizable).ColonizableManager;
            var currentColony = manager.CurrentColony;
            var cost = GetCost(manager, currentColony, otherColony);
            if (!deductedMaterials(currentColony, cost))
                return;

            var route = new TradeRoute((EGood)sentGood, sentAmount, (EGood)receivedGood, receivedAmount, currentColony, otherColony, Mathf.Sqrt(cost));
            currentColony.TradeManager.AddOutGoingRoute(route);
            otherColony.TradeManager.AddIncomingRoute(route);

            DisableFields();
            GoodsDialogController.Update(currentColony);
        }
        private float GetCost(IColonizableManager manager, Colony currentColony, Colony otherColony)
        {
            var colonyDistance = Utils.GetDistance(currentColony.Location, otherColony.Location, Utils.GetRowSize(manager.Size));
            return (colonyDistance / 10f) * Math.Max(sentAmount, receivedAmount);
        }
        private bool deductedMaterials(Colony currentColony, float cost)
        {
            var goods = currentColony.Goods;
            if (!(goods.ContainsKey(EGood.Steel) && goods[EGood.Steel].Value >= cost ||
                goods.ContainsKey(EGood.Chips) && goods[EGood.Chips].Value >= cost))
                return false;
            currentColony.IncrementGood(EGood.Steel, -cost);
            currentColony.IncrementGood(EGood.Chips, -cost);
            return true;
        }
        private Colony FindOtherColony()
        {
            foreach (var col in (Selection.CurrentSelection as IColonizable).ColonizableManager.Colonies)
                if (colonyNameField.text.Equals(col.Colony.Name))
                    return col.Colony;
            return null;
        }
        private bool ValidateSentGood()
        {
            var sentName = sentGoodField.text;
            sentGood = null;
            foreach (var pair in Constants.GOOD_MAP)
                if (sentName.Equals(pair.Value))
                    sentGood = pair.Key;
            if (sentGood == null || string.IsNullOrEmpty(sentAmountField.text))
                return false;
            sentAmount = float.Parse(sentAmountField.text);
            return true;
        }
        private bool ValidateReceivedGood()
        {
            var receivedName = receivedGoodField.text;
            receivedGood = null;
            foreach (var pair in Constants.GOOD_MAP)
                if (receivedName.Equals(pair.Value))
                    receivedGood = pair.Key;
            if (receivedGood == null || string.IsNullOrEmpty(receivedAmountField.text))
                return false;
            receivedAmount = float.Parse(receivedAmountField.text);
            return true;
        }
        public void Init(InputField x, InputField y, InputField z, InputField yN, InputField zN)
        {
            colonyNameField = x;
            sentGoodField = y;
            receivedGoodField = z;
            sentAmountField = yN;
            receivedAmountField = zN;
        }
        private void DisableFields()
        {
            colonyNameField.interactable = false;
            sentGoodField.interactable = false;
            receivedGoodField.interactable = false;
            receivedAmountField.interactable = false;
            sentAmountField.interactable = false;
            gameObject.SetActive(false);
        }
    }
}
