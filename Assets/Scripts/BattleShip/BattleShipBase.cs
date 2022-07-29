using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.BattleShip
{
    public abstract class BattleShipBase : MonoBehaviour
    {
        private Rigidbody m_Rigidbody;
        [SerializeField] private Transform propeller;

        [SerializeField] protected float maxSpeed = 10f;
        [SerializeField] protected float movePower = 5f;
        [SerializeField] protected float maxSteer = 5f;
        [SerializeField] protected float steerPower = 5f;
        [SerializeField] protected float steerAccel = 0.1f;
        [SerializeField] protected float steerResetSpeed = 0.1f;
        [SerializeField] protected int _gear = 0;
        [SerializeField] protected float _steer = 0;

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
            get { return m_Rigidbody.velocity.magnitude; }
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
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
    }
}