using System;
using TMPro;
using UnityEngine;
using WOW.Controller;

namespace WOW.UI
{
    public class Dashboard : MonoBehaviour
    {
        PlayerController playerController;
        public TextMeshProUGUI gearText;
        public TextMeshProUGUI velocityText;
        public TextMeshProUGUI steerText;

        // Start is called before the first frame update
        void Start()
        {
            playerController = GameObject.FindGameObjectWithTag("PlayerController")?.GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateGear();
            UpdateVelocity();
            UpdateSteer();
        }

        void UpdateGear()
        {
            gearText.text = String.Format("기어: {0}단", playerController.ship.Gear);
        }
        private void UpdateVelocity()
        {
            velocityText.text = String.Format("속력: {0:0.00}km/h", playerController.ship.Velocity * 60);
        }
        private void UpdateSteer()
        {
            steerText.text = String.Format("방향타: {0:0.0} 방향", playerController.ship.Steer);
        }
    }
}
