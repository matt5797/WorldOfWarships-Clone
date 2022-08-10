using UnityEngine;
using WOW.BattleShip;

namespace WOW.Controller
{
    public class ShipController : MonoBehaviour
    {
        public BattleShipBase ship;
        public Vector3 targetPoint;

        public Vector3 TargetPoint
        {
            get { return targetPoint; }
            protected set { targetPoint = value; }
        }
    }
}