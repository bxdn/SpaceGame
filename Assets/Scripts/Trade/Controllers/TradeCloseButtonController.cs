using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers
{
    class TradeCloseButtonController : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Constants.COLONY_PANEL.SetActive(true);
            Constants.TRADE_PANEL.SetActive(false);
        }
    }
}
