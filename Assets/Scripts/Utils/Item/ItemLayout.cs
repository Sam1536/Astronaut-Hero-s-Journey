using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Itens
{
    public class ItemLayout : MonoBehaviour
    {
        private ItensSetup _currentSetup;

        public Image uiIcon;
        public TextMeshProUGUI uiValue;

       public void Load(ItensSetup setup)
       {
            _currentSetup = setup;
            UpdateUi();
       }

        public void UpdateUi()
        {
            uiIcon.sprite = _currentSetup.icon;

        }

        private void Update()
        {
            uiValue.text = _currentSetup.sOint.value.ToString();
        }
    }

}