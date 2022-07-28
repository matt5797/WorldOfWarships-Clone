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
        [SerializeField] protected float steerResetSpeed = 0.1f;
        [SerializeField] protected int _gear = 0;
        [SerializeField] protected float _steer = 0;

        public bool isDetected = false;
        public bool isMovable = true;

        // For Debugging
        public Vector3 velocity;
        public Vector3 angularVelocity;

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
            _gear = Mathf.Clamp(_gear + 1, 0, 3);
        }
        public void GearDown()
        {
            _gear = Mathf.Clamp(_gear - 1, 0, 3);
        }

        public void SteerUp()
        {
            _steer = Mathf.Clamp(_steer + 0.1f * Time.deltaTime, -1, 1);
        }
        public void SteerDown()
        {
            _steer = Mathf.Clamp(_steer - 0.1f * Time.deltaTime, -1, 1);
        }

        private void Propeller()
        {
            m_Rigidbody.AddForceAtPosition(transform.forward * movePower / 4 * _gear * Time.deltaTime, propeller.position, ForceMode.Force);
        }

        private void Steering()
        {
            m_Rigidbody.AddForceAtPosition(transform.right * steerPower * _steer * Time.deltaTime, propeller.position, ForceMode.Force);
        }

        private void ResetSteer()
        {
            if (Mathf.Abs(_steer) < steerResetSpeed * Time.deltaTime)
            {
                _steer = 0;
            }
            else if (_steer < 0)
            {
                _steer += steerResetSpeed * Time.deltaTime;
            }
            else if (_steer > 0)
            {
                _steer -= steerResetSpeed * Time.deltaTime;
            }
        }
    }
}