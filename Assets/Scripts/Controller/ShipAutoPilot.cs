using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.BattleShip;
using UnityEngine.AI;
using UnityEngine.Events;

namespace WOW.Controller
{
    public class ShipAutoPilot : MonoBehaviour
    {
        public ShipController controller;
        Vector3 destination;
        Vector3 lookAtPoint;
        NavMeshPath path;
        NavMeshHit hit;
        public bool isMove = false;
        public bool isArrived = false;
        float distance, steer;
        public float pathTime = 0.5f;
        float pathCurrentTime;

        private void Awake()
        {
            controller = GetComponentInParent<ShipController>();
        }

        void Start()
        {
            hit = new NavMeshHit();
            path = new NavMeshPath();
        }

        void Update()
        {
            UpdateMove();
        }

        // 미세하게 앞뒤로 움직이며 목표 방향을 바라본다.
        IEnumerator Parking()
        {
            int _gear = 1;
            float _steerTime = 2f;
            float _steerTimer;
            while (Vector3.Dot((lookAtPoint-transform.position).normalized, transform.forward) < 0.95)
            {
                _gear *= -1;
                controller.ship.Gear = _gear;
                _steerTimer = _steerTime;
                while (_steerTimer > 0 && Vector3.Dot((lookAtPoint - transform.position).normalized, transform.forward) < 0.95)
                {
                    _steerTimer -= Time.deltaTime;
                    steer = SteerTowardsTarget(lookAtPoint);

                    if (steer <= 0)
                    {
                        controller.ship.SteerDown();
                    }
                    else if (steer >= -0)
                    {
                        controller.ship.SteerUp();
                    }
                    yield return null;
                }
            }
            controller.ship.Gear = 0;
            yield break;
        }

        void UpdateMove()
        {
            if (!isMove || destination == null)
                return;
            
            // 경로 계산
            if (pathCurrentTime <= 0)
            {
                pathCurrentTime = pathTime;
                NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            }
            else
            {
                pathCurrentTime -= Time.deltaTime;
            }
            
            if (path.status != NavMeshPathStatus.PathComplete || path.corners.Length<=1)
                return;

            // 타겟 방향으로 방향타를 조정
            steer = SteerTowardsTarget(path.corners[1]);

            if (steer >= 0)
            {
                controller.ship.SteerDown();
            }
            else if (steer <= -0)
            {
                controller.ship.SteerUp();
            }

            distance = Vector3.Distance(transform.position, path.corners[1]);
            
            // 다음 코너 거리에 따라 기어 변속
            if (distance < 1f && path.corners.Length <= 2)
            {
                // 목적지에 도착하면 종료
                OnDestination();
            }
            else if (distance < 10)
            {
                controller.ship.Gear = 2;
            }
            else if (distance < 50)
            {
                controller.ship.Gear = 3;
            }
            else
            {
                controller.ship.Gear = 4;
            }
        }

        void OnDestination()
        {
            controller.ship.Gear = 0;
            //StartCoroutine(Parking());
            SetActive(false);
        }

        public void SetActive(bool isActive)
        {
            if (isActive)
            {
                isMove = true;
                isArrived = false;
                StopAllCoroutines();
            }
            else
            {
                isMove = false;
                isArrived = true;
            }
        }

        public bool SetDestination(Vector3 destination)
        {
            if (NavMesh.SamplePosition(destination, out hit, 1000, 1))
            {
                this.destination = hit.position;
                StopAllCoroutines();
                return true;
            }
            return false;
        }
        
        public bool SetDestination(Vector3 destination, Vector3 lookAtPoint)
        {
            if (NavMesh.SamplePosition(destination, out hit, 1000, 1))
            {
                this.destination = hit.position;
                this.lookAtPoint = lookAtPoint;
                return true;
            }
            return false;
        }

        public float SteerTowardsTarget(Vector3 destination)
        {
            float distance = Vector3.Distance(destination, transform.position);

            if (distance > 1)
            {
                Vector3 desiredHeading = destination - transform.position;
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

        private void OnDrawGizmosSelected()
        {
            if (path!=null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(destination, 2);

                if (path.corners.Length > 1)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawSphere(path.corners[0], 3f);
                }

                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(path.corners[i], 0.5f);
                    Gizmos.DrawLine(path.corners[i], path.corners[(i + 1)]);
                }
            }

            if (lookAtPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(lookAtPoint, 2);
            }
        }
    }

}