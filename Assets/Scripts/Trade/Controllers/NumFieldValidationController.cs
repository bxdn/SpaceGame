using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class NumFieldValidationController : MonoBehaviour
    {
        private void Start()
        {
            var inField = gameObject.GetComponent<InputField>();
            inField.onValidateInput = delegate (string input, int charIndex, char addedChar) { return MyValidate(addedChar); };
        }
        private char MyValidate(char charToValidate)
        {
            if (charToValidate != '.' && !char.IsDigit(charToValidate))
                charToValidate = '\0';
            return charToValidate;
        }
    }
}
