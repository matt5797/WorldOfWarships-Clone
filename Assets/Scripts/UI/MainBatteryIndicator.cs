using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WOW.Armament;
using WOW.Controller;
using TMPro;

namespace WOW.UI
{

    public class MainBatteryIndicator : MonoBehaviour
    {
        PlayerController controller;
        MainBattery[] mainBatterys;
        public Image indicatorImage;
        TextMeshProUGUI numberText;
        Dictionary<MainBattery, Image> indicatorDict = new Dictionary<MainBattery, Image>();

        // Start is called before the first frame update
        void Start()
        {
            controller = GameObject.FindGameObjectWithTag("PlayerController")?.GetComponent<PlayerController>();
            mainBatterys = controller.ship.GetComponentsInChildren<MainBattery>();

            //foreach (MainBattery mainBattery in mainBatterys)
            for (int i = 0; i < mainBatterys.Length; i++)
            {
                Image indicator = Instantiate<Image>(indicatorImage);
                indicator.transform.SetParent(transform);
                indicator.GetComponentInChildren<TextMeshProUGUI>().text = (i+1).ToString();
                indicatorDict.Add(mainBatterys[i], indicator);
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            foreach (KeyValuePair<MainBattery, Image> pair in indicatorDict)
            {
                Vector3 point = Camera.main.WorldToScreenPoint(pair.Key.TargetingPoint);
                //point.y = Screen.height / 2;
                point.x = Mathf.Clamp(point.x, 0, Screen.width);
                pair.Value.transform.position = point;
            }
        }
    }
}