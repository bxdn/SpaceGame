using Assets.Scripts.Interfaces;
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
    public class RenameController : InputFieldController
    {
        public InputField inputField;
        private void Start()
        {
            inputField.onEndEdit = new InputField.SubmitEvent();
            inputField.onEndEdit.AddListener(UpdateColony);
        }
        private void UpdateColony(string name)
        {
            (Selection.CurrentSelection as IColonizable).ColonizableManager.CurrentColony.Name = inputField.text;
        }
    }
}
