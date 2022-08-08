using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOW.UI
{
    public class HPBar : MonoBehaviour
    {
        public Image front;

        public void SetHPPercent(float hpPercent)
        {
            front.fillAmount = hpPercent;
            //print(front.fillAmount);
        }
    }
}