using UnityEngine;
using WOW.BattleShip;
using UnityEngine.AI;
using WOW.Manager;

namespace WOW.Controller
{
    public class ShipController : MonoBehaviour
    {
        [HideInInspector] public BattleShipBase ship;
        [HideInInspector] public Vector3 targetPoint;
        [HideInInspector] public ShipAutoPilot autoPilot;
        [HideInInspector] public Admiral admiral;

        protected bool isAutoPilot = false;

        public Vector3 TargetPoint
        {
            get { return targetPoint; }
            set { targetPoint = value; }
        }

        protected virtual void Awake()
        {
            ship = GetComponentInChildren<BattleShipBase>();
            autoPilot = GetComponentInChildren<ShipAutoPilot>();
            admiral = GetComponentInParent<Admiral>();
        }

        protected virtual void Start()
        {
            
        }
    }
}