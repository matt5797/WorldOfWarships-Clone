using UnityEngine;
using System.Collections.Generic;
using WOW.Armament;
using WOW.Ability;

namespace WOW.BattleShip
{
    public enum BattleShipType
    {
        None,
        Destroyer,
        Cruiser,
        Battleship,
        AircraftCarrier,
    }

    [RequireComponent(typeof(Rigidbody))]
    public abstract class BattleShipBase : MonoBehaviour
    {
        private Rigidbody m_Rigidbody;
        [SerializeField] private Transform propeller;
        private AbilityBase[] abilities;
        private Dictionary<int, AbilityBase> m_AbilityDict = new Dictionary<int, AbilityBase>();

        [SerializeField] protected float maxSpeed = 10f;
        [SerializeField] protected float movePower = 5f;
        [SerializeField] protected float maxSteer = 5f;
        [SerializeField] protected float steerPower = 5f;
        [SerializeField] protected float steerAccel = 0.1f;
        [SerializeField] protected float steerResetSpeed = 0.1f;
        [SerializeField] protected int _gear = 0;
        [SerializeField] protected float _steer = 0;

        private int currentAbilityIndex;

        public bool isDetected = false;
        public bool isMovable = true;

        // For Debugging
        public Vector3 velocity;
        public Vector3 angularVelocity;

        public int Gear
        {
            get { return _gear; }
            set { _gear = value; }
        }
        public float Steer
        {
            get { return _steer; }
            set { _steer = value; }
        }
        public float Velocity
        {
            get
            { return m_Rigidbody.velocity.magnitude; }
        }

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();

            ResetAbilityDict();
        }
        
        // Start is called before the first frame update
        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {
            velocity = m_Rigidbody.velocity;
            angularVelocity = m_Rigidbody.angularVelocity;
        }

        // Update is called once per frame
        protected virtual void FixedUpdate()
        {
            if (isMovable)
            {
                Propeller();
                Steering();
            }
            if (m_Rigidbody.velocity.magnitude > maxSpeed)
            {
                m_Rigidbody.velocity = Vector3.ClampMagnitude(m_Rigidbody.velocity, maxSpeed);
            }
            if (m_Rigidbody.angularVelocity.magnitude > maxSteer)
            {
                m_Rigidbody.angularVelocity = Vector3.ClampMagnitude(m_Rigidbody.angularVelocity, maxSteer);
            }
            ResetSteer();
        }

        public void GearUp()
        {
            Gear = Mathf.Clamp(Gear + 1, -1, 4);
        }
        public void GearDown()
        {
            Gear = Mathf.Clamp(Gear - 1, -1, 4);
        }

        public void SteerUp()
        {
            Steer = Mathf.Clamp(Steer + steerAccel * Time.deltaTime, -1, 1);
        }
        public void SteerDown()
        {
            Steer = Mathf.Clamp(Steer - steerAccel * Time.deltaTime, -1, 1);
        }

        private void Propeller()
        {
            m_Rigidbody.AddForceAtPosition(transform.forward * movePower / 4 * Gear * Time.deltaTime, propeller.position, ForceMode.Force);
        }

        private void Steering()
        {
            m_Rigidbody.AddForceAtPosition(transform.right * steerPower * Steer * Time.deltaTime, propeller.position, ForceMode.Force);
        }

        private void ResetSteer()
        {
            if (Mathf.Abs(Steer) < steerResetSpeed * Time.deltaTime)
            {
                Steer = 0;
            }
            else if (Steer < 0)
            {
                Steer += steerResetSpeed * Time.deltaTime;
            }
            else if (Steer > 0)
            {
                Steer -= steerResetSpeed * Time.deltaTime;
            }
        }

        public void TriggerAbility()
        {
            m_AbilityDict[currentAbilityIndex].TriggerAbility();
        }

        public void ChangeArmament(int index)
        {
            if (m_AbilityDict.ContainsKey(currentAbilityIndex))
            {
                currentAbilityIndex = index;
                // °íÆøÅº¿¡¼­ Ã¶°©ÅºÀÇ °æ¿ì ÅºÈ¯ º¯°æ¿¡ ´ëÇÑ ÄÚµå Ãß°¡
            }
        }

        void ResetAbilityDict()
        {
            abilities = GetComponentsInChildren<AbilityBase>();
            for (int i = 0; i < abilities.Length; i++)
            {
                m_AbilityDict.Add(i, abilities[i]);
            }
            currentAbilityIndex = 1;
        }
    }
}