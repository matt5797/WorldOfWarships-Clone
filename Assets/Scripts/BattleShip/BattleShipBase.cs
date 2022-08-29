using UnityEngine;
using System.Collections.Generic;
using WOW.Armament;
using WOW.Ability;
using UnityEngine.Events;
using System.Collections;

namespace WOW.BattleShip
{
    public enum Camp
    {
        Allies, // 연합국
        Axis    // 추축국
    }
    
    public enum BattleShipType
    {
        Destroyer,  // 구축함
        Cruiser,    // 순양함
        Battleship, // 전함
        AircraftCarrier,    //항공모함
    }

    [RequireComponent(typeof(Rigidbody))]
    public abstract class BattleShipBase : MonoBehaviour
    {
        public Camp camp;
        [HideInInspector] public BattleShipType shipType;
        
        private Rigidbody m_Rigidbody;
        [SerializeField] private Transform propeller;
        private AbilityBase[] abilities;
        public Dictionary<int, AbilityBase> m_AbilityDict = new Dictionary<int, AbilityBase>();

        [SerializeField] protected float maxSpeed = 10f;
        [SerializeField] protected float movePower = 5f;
        [SerializeField] protected float maxSteer = 5f;
        [SerializeField] protected float steerPower = 5f;
        [SerializeField] protected float steerAccel = 0.1f;
        [SerializeField] protected float steerResetSpeed = 0.1f;
        [SerializeField] protected int _gear = 0;
        [SerializeField] protected float _steer = 0;
        //[SerializeField] protected Quaternion rot = default;

        public int currentAbilityIndex;

        public bool isDetected = false;
        public bool isMovable = true;
        public bool isSteerable = true;
        public bool isDead = false;

        // For Debugging
        public Vector3 velocity;
        public Vector3 angularVelocity;

        public UnityEvent<int> onAbilityChange;

        public float attackRange = 10f;

        public ParticleSystem[] deadEffects;
        public GameObject marker;
        public MainBattery[] mainBatteries;

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

        public float AttackRange
        {
            get { return attackRange; }
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
            print("GearUp");
            if (isMovable)
                Gear = Mathf.Clamp(Gear + 1, -1, 4);
        }
        public void GearDown()
        {
            if (isMovable)
                Gear = Mathf.Clamp(Gear - 1, -1, 4);
        }

        public void SteerUp()
        {
            if (isSteerable)
                Steer = Mathf.Clamp(Steer + steerAccel * Time.deltaTime, -1, 1);
        }
        public void SteerDown()
        {
            if (isSteerable)
                Steer = Mathf.Clamp(Steer - steerAccel * Time.deltaTime, -1, 1);
        }

        private void Propeller()
        {
            if (isDead)
                return;
            
            Vector3 dir = transform.forward * movePower / 4 * Gear * Time.deltaTime;
            Quaternion rot = Quaternion.Euler(0, Mathf.Clamp(Steer * steerPower * 90, -90, 90) /** Time.deltaTime*/, 0);
            m_Rigidbody.AddForceAtPosition(rot * dir, propeller.position, ForceMode.Force);

        }

        /*private void Steering()
        {
            //m_Rigidbody.AddForceAtPosition(transform.right * steerPower * Steer * Time.deltaTime, propeller.position, ForceMode.Force);
        }*/

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
            if (m_AbilityDict.ContainsKey(index))
            {
                m_AbilityDict[index].OnAbilityEnabled();
                m_AbilityDict[currentAbilityIndex].OnAbilityDisabled();
                currentAbilityIndex = index;
                onAbilityChange.Invoke(index);
            }
        }

        void ResetAbilityDict()
        {
            abilities = GetComponentsInChildren<AbilityBase>();
            for (int i = 0; i < abilities.Length; i++)
            {
                m_AbilityDict.Add(i+1, abilities[i]);
            }
            currentAbilityIndex = 1;
        }
        
        /// <summary>
        /// 몇 초 뒤 위치를 반환
        /// </summary>
        /// <param name="_predictiontime">초</param>
        /// <returns></returns>
        public Vector3 PredictionPos(float _predictiontime)
        {
            //get the rigidbodies velocity
            Vector3 _targvelocity = m_Rigidbody.velocity;
            //multiply it by the amount of seconds you want to see into the future
            _targvelocity *= _predictiontime;
            //add it to the rigidbodies position
            _targvelocity += m_Rigidbody.position;
            //Return the position of where the target will be in the amount of seconds you want to see into the future
            return _targvelocity;
        }

        /// <summary>
        /// 몇 초 뒤 회전각도를 반환
        /// </summary>
        /// <param name="_predictiontime">초</param>
        /// <returns></returns>
        public Quaternion PredictionRot(float _predictiontime)
        {
            return Quaternion.Euler(transform.InverseTransformVector(angularVelocity) * Mathf.Rad2Deg * _predictiontime);
        }

        private void OnDrawGizmosSelected()
        {
            if (!m_Rigidbody)
                return;
            
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(PredictionPos(3), 0.3f);
            Gizmos.DrawSphere(PredictionPos(5), 0.5f);
            Gizmos.DrawSphere(PredictionPos(7), 0.7f);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, PredictionRot(3) * transform.forward * 100);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, PredictionRot(5) * transform.forward * 100);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, PredictionRot(7) * transform.forward * 100);
        }

        public void OnDead()
        {
            if (!isDead)
            {
                isDead = true;
                DisableMarker();
                StartCoroutine(OnDeadEffect());
            }
        }

        IEnumerator OnDeadEffect()
        {
            yield return new WaitForSeconds(2);
            foreach (ParticleSystem effect in deadEffects)
            {
                effect.Play();
            }
            yield return new WaitForSeconds(5);
            while (true)
            {
                transform.position -= Vector3.up * Time.deltaTime;
                yield return new WaitForSeconds(0.1f);
                if (transform.position.y < -10)
                {
                    break;
                }
            }
            yield break;
        }

        void DisableMarker()
        {
            if (marker)
            {
                marker.SetActive(false);
            }
        }

        public void OnEngineBreak()
        {
            isMovable = false;
        }

        public void OnEngineRecover()
        {
            isMovable = true;
        }
        
        public void OnSteerBreak()
        {
            isSteerable = false;
        }

        public void OnSteerRecover()
        {
            isSteerable = true;
        }
    }
}