using UnityEngine;
using WOW.BattleShip;

namespace WOW.Controller
{
    public class PlayerController : MonoBehaviour
    {
        public BattleShipBase ship;
        public Vector3 targetPoint;

        Ray cameraCenterRay;
        RaycastHit cameraCenterHit;
        public float rayMaxDistance = 1000;
        public LayerMask targetLayerMask;

        public Vector3 TargetPoint
        {
            get { return targetPoint; }
            private set { targetPoint = value; }
        }
        
        // Update is called once per frame
        void Update()
        {
            // Set Target from Camera Point
            cameraCenterRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            
            if (Physics.Raycast(cameraCenterRay, out cameraCenterHit, rayMaxDistance, targetLayerMask))
            {
                TargetPoint = cameraCenterHit.point;
            }
            else
            {
                TargetPoint = Camera.main.transform.position + Camera.main.transform.forward * rayMaxDistance;
            }

            // Ship Movement
            if (Input.GetKeyDown(KeyCode.W))
            {
                ship.GearUp();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                ship.GearDown();
            }
            if (Input.GetKey(KeyCode.A))
            {
                ship.SteerDown();
            }
            if (Input.GetKey(KeyCode.D))
            {
                ship.SteerUp();
            }

            // Ship Attack
            if (Input.GetMouseButtonDown(0))
            {
                ship.TriggerAbility();
            }

            // Skill Change
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ship.ChangeArmament(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ship.ChangeArmament(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ship.ChangeArmament(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ship.ChangeArmament(4);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(TargetPoint, 0.3f);
        }
    }
}