using UnityEngine;
using WOW.BattleShip;
using UnityEngine.AI;

namespace WOW.Controller
{
    public class ShipController : MonoBehaviour
    {
        public BattleShipBase ship;
        public Vector3 targetPoint;
        public ShipAutoPilot autoPilot;
        protected NavMeshAgent agent;

        protected bool isAutoPilot = false;

        public Vector3 TargetPoint
        {
            get { return targetPoint; }
            protected set { targetPoint = value; }
        }

        protected virtual void Start()
        {
            ship = GetComponentInChildren<BattleShipBase>();
            autoPilot = GetComponentInChildren<ShipAutoPilot>();
            agent = GetComponentInChildren<NavMeshAgent>();
        }
    }
}