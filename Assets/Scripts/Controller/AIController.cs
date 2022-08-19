using UnityEngine;
using WOW.BattleShip;

namespace WOW.Controller
{
    /*public enum AIState
    {
        Idle,
        Move,
        Attack,
        Siege
    }*/
    
    public class AIController : ShipController
    {
        public State<AIController> AIState;
        protected override void Start()
        {
            base.Start();
            AIState = new IdleState();
        }

        private void Update()
        {
            State<AIController> nowState = AIState.InputHandle(this);
            print(GetInstanceID() + " / "+ nowState);
            AIState.action(this);

            if (!nowState.Equals(AIState))
            {
                print(AIState + " to " + nowState);
                AIState.Exit(this);
                AIState = nowState;
            }
        }

        private void FixedUpdate()
        {
            AIState.FixedUpdate(this);
        }

        private void LateUpdate()
        {
            AIState.LateUpdate(this);
        }

        public BattleShipBase GetClosestEnemy()
        {
            float minDistance = float.MaxValue;
            BattleShipBase closetShip = null;
            foreach (BattleShipBase ship in admiral.enemy.battleShips)
            {
                float distance = Vector3.Distance(ship.transform.position, ship.transform.position);
                if (distance<minDistance)
                {
                    minDistance = distance;
                    closetShip = ship;
                }
            }
            return closetShip;
        }
    }
}