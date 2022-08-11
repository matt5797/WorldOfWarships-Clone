using UnityEngine;
using WOW.BattleShip;

namespace WOW.Controller
{
    public class EnemyController : ShipController
    {
        /*public Transform player;
        public float attackTime = 1.5f;
        public float currentTime;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            TargetPoint = player.position;

            // Ship Attack
            if (attackTime<currentTime)
            {
                currentTime = 0;
                ship.TriggerAbility();
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(TargetPoint, 0.3f);
        }*/
    }
}