using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Controller
{
    public class ShipAutoPilot : MonoBehaviour
    {
        public ShipController controller;
        public Vector3[] wayPoints;
        int currentWayPoint = 0;

        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponentInParent<ShipController>();

            controller.ship.GearUp();
            controller.ship.GearUp();
            controller.ship.GearUp();
            controller.ship.GearUp();
        }

        // Update is called once per frame
        void Update()
        {
            if (wayPoints.Length == 0)
                return;

            if (Vector3.Distance(wayPoints[currentWayPoint], transform.position) < 1)
            {
                currentWayPoint++;
                if (currentWayPoint >= wayPoints.Length)
                {
                    print("도착");
                    currentWayPoint = 0;
                    wayPoints = null;
                }
            }

            // 타겟 방향으로 방향타를 조정
            float steer = SteerTowardsTarget(wayPoints[currentWayPoint]);
            if (steer >= 0)
            {
                if (Random.value<=Mathf.Abs(steer))
                    controller.ship.SteerDown();
            }
            if (steer <= -0)
            {
                if (Random.value <= Mathf.Abs(steer))
                    controller.ship.SteerUp();
            }
        }

        public float SteerTowardsTarget(Vector3 destination)
        {
            float distance = Vector3.Distance(destination, transform.position);

            if (distance > 1)
            {
                Vector3 desiredHeading = destination - transform.position;
                Vector3 currentHeading = transform.forward;

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