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

        private void Awake()
        {
            battleShips = GetComponentsInChildren<BattleShipBase>();
            controllers = GetComponentsInChildren<AIController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            controllers[0].AIState = new MoveState(GameObject.Find("Test Target 2").transform.position, GameObject.Find("Test Target 2 (1)").transform.position);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

    }
}