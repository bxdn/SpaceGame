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
        private InputField x;
        private InputField y;
        private InputField z;
        private InputField yN;
        private InputField zN;
        public void OnPointerClick(PointerEventData eventData)
        {
            x.interactable = false;
            y.interactable = false;
            z.interactable = false;
            zN.interactable = false;
            yN.interactable = false;
        }
        public void Init(InputField x, InputField y, InputField z, InputField yN, InputField zN)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.yN = yN;
            this.zN = zN;
        }
    }
}
