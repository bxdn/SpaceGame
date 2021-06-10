using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using Assets.Scripts.Registry;
using Assets.Scripts.Trade.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Assets.Scripts.Registry.GoodsServicesRegistry;

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
        private GoodOrService sentGood;
        private GoodOrService receivedGood;
        private Colony otherColony = null;

        public void OnPointerClick(PointerEventData eventData)
        {
            var cost = GetCost();
            if (cost == -1)
                return;
            var manager = (Selection.CurrentSelection as IColonizable).ColonizableManager;
            var currentColony = manager.CurrentColony;
            if (currentColony == otherColony)
                return;
            if (!DeductedMaterials(currentColony, cost))
                return;
            var route = new TradeRoute(sentGood, sentAmount, receivedGood, receivedAmount, currentColony, otherColony, Mathf.Sqrt(cost));
            currentColony.TradeManager.AddOutGoingRoute(route);
            otherColony.TradeManager.AddIncomingRoute(route);
            TradePanelController.Reset(currentColony);
            GoodsDialogController.Update(currentColony);
        }
        private bool ValidateGoodsAndColony()
        {
            return ValidateSentGood() && ValidateReceivedGood() && (otherColony = FindOtherColony()) != null;
        } 
        public float GetCost()
        {
            if (!ValidateGoodsAndColony())
                return -1;
            var manager = (Selection.CurrentSelection as IColonizable).ColonizableManager;
            var colonyDistance = Utils.GetDistance(manager.CurrentColony.Location, otherColony.Location, Utils.GetRowSize(manager.Size));
            return (colonyDistance / 10f) * Math.Max(sentAmount, receivedAmount);
        }
        private bool DeductedMaterials(Colony currentColony, float cost)
        {
            var goods = currentColony.Goods;
            var steel = RegistryUtil.GoodsServices.Get("Steel");
            var chips = RegistryUtil.GoodsServices.Get("Chips");
            if (!(goods.ContainsKey(steel) && goods[steel].Value >= cost ||
                goods.ContainsKey(chips) && goods[chips].Value >= cost))
                return false;
            currentColony.IncrementGood(steel, -cost);
            currentColony.IncrementGood(chips, -cost);
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
            foreach (var good in RegistryUtil.GoodsServices.GetAllGoods())
                if (sentName.Equals(good.Name))
                    sentGood = good;
            if (sentGood == null || string.IsNullOrEmpty(sentAmountField.text))
                return false;
            sentAmount = float.Parse(sentAmountField.text);
            return true;
        }
        private bool ValidateReceivedGood()
        {
            var receivedName = receivedGoodField.text;
            receivedGood = null;
            foreach (var good in RegistryUtil.GoodsServices.GetAllGoods())
                if (receivedName.Equals(good.Name))
                    receivedGood = good;
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
    }
}
