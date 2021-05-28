using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Trade.Controllers
{
    public class CostNotifyController : MonoBehaviour
    {
        public void Start()
        {
            gameObject.GetComponent<InputField>().onEndEdit.AddListener(UpdateCostFields);
        }
        private void UpdateCostFields(string name)
        {
            TradePanelController.NotifyChanged();
        }
    }
}
