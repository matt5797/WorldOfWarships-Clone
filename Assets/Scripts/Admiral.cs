using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.BattleShip;
using WOW.Controller;

namespace WOW
{
    public class Admiral : MonoBehaviour
    {
        public Camp camp;
        public Admiral enemy;
        [HideInInspector] public AIController[] controllers;
        [HideInInspector] public BattleShipBase[] battleShips;
        public Zone[] zones;

        private void Awake()
        {
            battleShips = GetComponentsInChildren<BattleShipBase>();
            controllers = GetComponentsInChildren<AIController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            //controllers[0].AIState = new MoveState(GameObject.Find("Test Target 2").transform.position, GameObject.Find("Test Target 2 (1)").transform.position);
            //controllers[0].AIState = new MoveState(GameObject.Find("Test Target 2").transform.position);
            // 임시로 가장 가까운 거점을 점거
            foreach (var controller in controllers)
            {
                float closetDistance = float.MaxValue;
                Zone closestZone = null;

                foreach (Zone zone in zones)
                {
                    float distance = Vector3.Distance(zone.transform.position, controller.ship.transform.position);
                    if (distance < closetDistance)
                    {
                        closetDistance = distance;
                        closestZone = zone;
                    }
                }
                if (closestZone != null)
                {
                    controller.newState = new OccupyState(closestZone);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }

    }
}