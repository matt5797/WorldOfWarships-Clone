using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WOW.Armament;
using WOW.Controller;
using WOW.Ability;
using TMPro;

namespace WOW.UI
{
    public class AbilityPanel : MonoBehaviour
    {
        PlayerController controller;
        //public Text text;
        public AbilityIcon AbilityIcon;
        List<AbilityIcon> icons = new List<AbilityIcon>();
        int currentIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            controller = GameObject.FindGameObjectWithTag("PlayerController")?.GetComponent<PlayerController>();
            controller.ship.onAbilityChange.AddListener(ChangeAbility);
            //controller.ship.m_AbilityDict
            foreach (KeyValuePair<int, AbilityBase> pair in controller.ship.m_AbilityDict)
            {
                //pair.Value.icon
                AbilityIcon icon = Instantiate<AbilityIcon>(AbilityIcon, transform);
                icon.GetComponent<Image>().sprite = pair.Value.icon;
                icons.Add(icon);
            }
            icons[0].SetSelect(true);
        }

        void ChangeAbility(int index)
        {
            icons[currentIndex].SetSelect(false);
            icons[index-1].SetSelect(true);
            currentIndex = index - 1;
        }
    }
}