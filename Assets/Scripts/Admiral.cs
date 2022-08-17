using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.BattleShip;

namespace WOW
{
    public class Admiral : MonoBehaviour
    {
        public Camp camp;
        public Admiral enemy;
        [HideInInspector] public BattleShipBase[] battleShips;
        
        private void Awake()
        {
            battleShips = GetComponentsInChildren<BattleShipBase>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

    }
}