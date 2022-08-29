using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Controller;
using WOW.Armament;

namespace WOW.UI
{
    public class ShipState : MonoBehaviour
    {
        public PlayerController playerController;
        MainBattery[] batterys;
        rotate[] rotates;
        
        // Start is called before the first frame update
        void Start()
        {
            batterys = playerController.ship.mainBatteries;
            rotates = GetComponentsInChildren<rotate>();

            rotates[0].main = playerController.ship.gameObject;

            for (int i = 1; i < rotates.Length; i++)
            {
                if (i <= batterys.Length)
                    rotates[i].main = batterys[i-1].gameObject;
                else
                    rotates[i].gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}