using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.BattleShip;
using UnityEngine.AI;
using UnityEngine.Events;
using WOW.Armament;

namespace WOW.Controller
{
    public class ShipAutoFire : MonoBehaviour
    {
        public ShipController controller;
        MainBattery[] batterys;
        TorpedoTube[] tubes;
        public bool isAutoFire = false;
        public float fireInterval = 1;

        private void Awake()
        {
            controller = GetComponentInParent<ShipController>();
            batterys = GetComponentsInChildren<MainBattery>();
            tubes = GetComponentsInChildren<TorpedoTube>();
        }

        void Start()
        {
            StartCoroutine(Fire());
        }

        IEnumerator Fire()
        {
            while (isAutoFire)
            {
                foreach (var battery in batterys)
                {
                    battery.TryFire();
                }
                foreach (var tube in tubes)
                {
                    tube.TryFire();
                }
                yield return new WaitForSeconds(fireInterval);
            }
        }
    }

}