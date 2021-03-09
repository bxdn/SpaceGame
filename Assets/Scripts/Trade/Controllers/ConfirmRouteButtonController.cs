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
        private InputField sentAmountField;
        private InputField receivedGoodField;
        private InputField receivedAmountField;
        public void OnPointerClick(PointerEventData eventData)
        {
            
            var sentName = sentGoodField.text;
            EGood? sentGood = null;
            foreach (var pair in Constants.GOOD_MAP)
                if (sentName.Equals(pair.Value))
                    sentGood = pair.Key;
            if (sentGood == null || string.IsNullOrEmpty(receivedGoodField.text))
                return;
            var sentAmount = float.Parse(receivedGoodField.text);

            var receivedName = sentAmountField.text;
            EGood? receivedGood = null;
            foreach (var pair in Constants.GOOD_MAP)
                if (receivedName.Equals(pair.Value))
                    receivedGood = pair.Key;
            if (receivedGood == null || string.IsNullOrEmpty(receivedAmountField.text))
                return;
            var receivedAmount = float.Parse(receivedAmountField.text);

            Colony otherColony = null;
            foreach (var col in WorldGeneration.Galaxy.Player.Colonies)
                if (colonyNameField.text.Equals(col.Name))
                    otherColony = col;
            if (otherColony == null)
                return;
            var currentColony = (Selection.CurrentSelection as IColonizable).ColonizableManager.CurrentColony;
            var route = new TradeRoute((EGood)sentGood, sentAmount, (EGood)receivedGood, receivedAmount, currentColony, otherColony);
            currentColony.TradeManager.AddOutGoingRoute(route);
            otherColony.TradeManager.AddIncomingRoute(route);

            colonyNameField.interactable = false;
            sentGoodField.interactable = false;
            sentAmountField.interactable = false;
            receivedAmountField.interactable = false;
            receivedGoodField.interactable = false;
            gameObject.SetActive(false);
        }
        public void Init(InputField x, InputField y, InputField z, InputField yN, InputField zN)
        {
            colonyNameField = x;
            sentGoodField = y;
            sentAmountField = z;
            receivedGoodField = yN;
            receivedAmountField = zN;
        }
    }
}
