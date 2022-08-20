using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOW.UI

{
    public class AbilityIcon : MonoBehaviour
    {
        public Image icon;
        public Image select;

        public void SetSelect(bool isSelected)
        {
            select.gameObject.SetActive(isSelected);
        }
    }
}