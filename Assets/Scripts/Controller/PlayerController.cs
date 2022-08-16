using UnityEngine;
using WOW.BattleShip;
using WOW.DamageSystem;
using WOW.UI;
using UnityEngine.AI;

namespace WOW.Controller
{
    public class PlayerController : ShipController
    {
        Ray cameraCenterRay;
        RaycastHit cameraCenterHit;
        public float rayMaxDistance = 1000;
        public LayerMask targetLayerMask;

        public GameObject autoPilotTarget;

        protected override void Start()
        {
            base.Start();
            
            Destroy(GetComponentInChildren<Canvas>().gameObject);
            GameObject.FindGameObjectWithTag("PlayerHPBar");
            GetComponentInChildren<DamageableManager>().hpBar = GameObject.FindGameObjectWithTag("PlayerHPBar")?.GetComponent<HPBar>();

            SetAutoPilot();
        }

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
                isAutoPilot = false;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                ship.GearDown();
                isAutoPilot = false;
            }
            if (Input.GetKey(KeyCode.A))
            {
                ship.SteerDown();
                isAutoPilot = false;
            }
            if (Input.GetKey(KeyCode.D))
            {
                ship.SteerUp();
                isAutoPilot = false;
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

        void SetAutoPilot()
        {
            isAutoPilot = true;

            /*NavMeshPath path = new NavMeshPath();

            NavMesh.CalculatePath(transform.position, autoPilotTarget.transform.position, NavMesh.AllAreas, path);

            //path.status = NavMeshPathStatus.PathComplete;
            foreach (Vector3 corner in path.corners)
            {
                print(corner);
            }
            //Vector3[] wayPoints = new Vector3[1] { autoPilotTarget.transform.position };
            //autoPilot.SetWayPoint(path.corners);
*/
            autoPilot.SetDestination(autoPilotTarget.transform.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(TargetPoint, 0.1f);
        }
    }
}