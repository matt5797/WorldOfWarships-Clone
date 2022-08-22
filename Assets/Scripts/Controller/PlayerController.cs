using UnityEngine;
using WOW.BattleShip;
using WOW.DamageSystem;
using WOW.UI;
using UnityEngine.AI;
using UnityEngine.UI;

namespace WOW.Controller
{
    public class PlayerController : ShipController
    {
        Ray cameraCenterRay;
        RaycastHit cameraCenterHit;
        public float rayMaxDistance = 1000;
        public LayerMask targetLayerMask;

        public GameObject autoPilotTarget;
        public float pathTime = 0.5f;
        float pathCurrentTime;

        public GameObject DeadUI;

        protected override void Start()
        {
            base.Start();
            
            Destroy(GetComponentInChildren<Canvas>().gameObject);
            GameObject.FindGameObjectWithTag("PlayerHPBar");
            DamageableManager damageableManager = GetComponentInChildren<DamageableManager>();
            damageableManager.hpBar = GameObject.FindGameObjectWithTag("PlayerHPBar")?.GetComponent<HPBar>();

            //StartAutoPilot();
            damageableManager.onDead.AddListener(() =>
            {
                DeadUI.SetActive(true);
            });
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
                ship.SteerUp();
                isAutoPilot = false;
            }
            if (Input.GetKey(KeyCode.D))
            {
                ship.SteerDown();
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

            // Auto Pillot
            if (isAutoPilot)
            {
                if (pathCurrentTime <= 0)
                {
                    pathCurrentTime = pathTime;
                    autoPilot.SetDestination(autoPilotTarget.transform.position);
                }
                else
                {
                    pathCurrentTime -= Time.deltaTime;
                }
            }
           
           
        }

        void StartAutoPilot()
        {
            isAutoPilot = true;
            autoPilot.SetActive(true);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(TargetPoint, 1f);
        }
    }
}