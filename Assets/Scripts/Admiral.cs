using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.BattleShip;
using WOW.Controller;
using UnityEngine.Events;

namespace WOW
{
    public class Admiral : MonoBehaviour
    {
        public Camp camp;
        public Admiral enemy;
        [HideInInspector] public AIController[] controllers;
        [HideInInspector] public BattleShipBase[] battleShips;
        public Zone[] zones;
        bool isExterminate = false;
        public UnityEvent onExterminate;

        Dictionary<AIController, Zone> targetZone = new Dictionary<AIController, Zone>();

        public GameObject endUI;

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

            foreach (var controller in controllers)
            {
                controller.ship.camp = camp;
            }

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
                    targetZone.Add(controller, closestZone);
                    controller.newState = new OccupyState(closestZone);
                }
            }

            onExterminate.AddListener(() =>
            {
                endUI.SetActive(true);
            });

            StartCoroutine(ChangeZone());
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!isExterminate)
            {
                bool isAllDead = true;
                foreach (BattleShipBase battleShip in battleShips)
                {
                    if (!battleShip.isDead)
                    {
                        isAllDead = false;
                        break;
                    }
                }
                if (isAllDead)
                    OnExterminate();
            }
        }

        void OnExterminate()
        {
            isExterminate = true;
            onExterminate.Invoke();
        }

        IEnumerator ChangeZone()
        {
            while (true)
            {
                yield return new WaitForSeconds(600f);
                Dictionary<AIController, Zone> targetZoneCopy = new Dictionary<AIController, Zone>(targetZone);
                foreach (KeyValuePair<AIController, Zone> pair in targetZone)
                {
                    foreach (Zone zone in zones)
                    {
                        if (pair.Value != zone)
                        {
                            targetZoneCopy[pair.Key] = zone;
                            pair.Key.newState = new OccupyState(zone);
                        }
                    }
                }
                targetZone = targetZoneCopy;
            }
        }
    }
}