using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.BattleShip;
using UnityEngine.AI;

namespace WOW.Controller
{
    public class ShipAutoPilot : MonoBehaviour
    {
        public ShipController controller;
        public Vector3[] wayPoints;
        int currentWayPoint = 0;
        bool isEnabled = false;
        float distance, steer;

        private void Awake()
        {
            controller = GetComponentInParent<ShipController>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!isEnabled || wayPoints.Length == 0)
                return;

            distance = Vector3.Distance(transform.position, wayPoints[currentWayPoint]);

            // 코너링
            if (distance < Vector3.Distance(transform.position, controller.ship.PredictionPos(10)))
            {
                if (currentWayPoint < wayPoints.Length - 1)
                {
                    // 다음 타겟 방향으로 방향타를 조정
                    steer = SteerTowardsTarget(wayPoints[currentWayPoint + 1]);
                }
            }
            else
            {
                // 타겟 방향으로 방향타를 조정
                steer = SteerTowardsTarget(wayPoints[currentWayPoint]);
            }

            if (steer >= 0)
            {
                controller.ship.SteerDown();
            }
            else if (steer <= -0)
            {
                controller.ship.SteerUp();
            }

            // 출발시 최대로 가속
            if (currentWayPoint == 0)
            {
                controller.ship.Gear = 4;
            }
            // 거리에 따라 기어 변속
            else if (currentWayPoint == wayPoints.Length - 1)
            {
                if (distance < Vector3.Distance(transform.position, controller.ship.PredictionPos(10)))
                {
                    controller.ship.Gear = 1;
                }
            }

            // 가까워지면 다음 웨이포인트로 이동
            if (Vector3.Distance(wayPoints[currentWayPoint], transform.position) < 1)
                NextWayPoint();
        }

        public void SetWayPoint(Vector3[] wayPoints)
        {
            this.wayPoints = wayPoints;
            isEnabled = true;
        }

        void NextWayPoint()
        {
            currentWayPoint++;
            if (currentWayPoint >= wayPoints.Length)
            {
                print("도착");
                controller.ship.Gear = 0;
                isEnabled = false;
            }
        }

        public float SteerTowardsTarget(Vector3 destination)
        {
            float distance = Vector3.Distance(destination, transform.position);

            if (distance > 1)
            {
                Vector3 desiredHeading = destination - transform.position;
                //Vector3 currentHeading = transform.forward;
                Vector3 currentHeading = controller.ship.PredictionRot(5) * transform.forward;

                Debug.DrawLine(transform.position, destination, Color.blue);
                float angleToDestination = Vector3.SignedAngle(currentHeading, desiredHeading, Vector3.up);
                return Mathf.Clamp(angleToDestination / 90f, -1, 1);
            }
            else
            {
                return 0;
            }

        }
    }

}