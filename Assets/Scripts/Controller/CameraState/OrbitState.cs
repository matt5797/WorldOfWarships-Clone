using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Controller
{
    public class OrbitState : State<CameraController>
    {
        private float m_X = 0.0f;
        private float m_Y = 0.0f;

        public override State<CameraController> InputHandle(CameraController t)
        {
            if (t.targetOffset.y <= t.stateChangeY)
            {
                return new EllipsoidState();
            }
            return this;
        }

        public override void Enter(CameraController t)
        {
            base.Enter(t);
            Vector3 angles = t.transform.eulerAngles;
            m_X = angles.y;
            m_Y = angles.x;

            t.targetOffset = new Vector3(0, t.stateChangeY + 0.05f, 0);
            t.cameraOffset = Vector3.zero; //new Vector3(0, t.cameraOffset.y, 0);

            t.m_XSpeed = 20;
        }

        public override void Update(CameraController t)
        {
            base.Update(t);
        }
        public override void LateUpdate(CameraController t)
        {
            base.LateUpdate(t);
            if (t.m_Target && !t.isChangingState)
            {
                t.targetOffset.y = Mathf.Clamp(t.targetOffset.y - (Input.GetAxis("Mouse ScrollWheel") * t.targetOffsetYSpeed), t.targetOffsetYMin, t.targetOffsetYMax);

                m_X += Input.GetAxis("Mouse X") * t.m_XSpeed * t.m_Distance * 0.02f;
                m_Y -= Input.GetAxis("Mouse Y") * t.m_YSpeed * 0.02f;

                m_Y = ClampAngle(m_Y, t.m_YMinLimit, t.m_YMaxLimit);

                Quaternion rotation = Quaternion.Euler(m_Y, m_X, 0);

                float distance = Vector3.Distance(t.m_Target.position + t.targetOffset, t.transform.position + t.cameraOffset);
                t.m_Distance = Mathf.Clamp(t.m_Distance - Input.GetAxis("Mouse ScrollWheel") * distance, t.m_DistanceMin, t.m_DistanceMax);

                Vector3 negDistance = new Vector3(0.0f, 0.0f, -t.m_Distance);
                Vector3 position = rotation * negDistance + t.m_Target.position + t.targetOffset;

                t.transform.rotation = rotation;
                t.transform.position = position + t.cameraOffset
                    + (t.transform.forward * t.cameraOffset2.z);
            }
        }

        public override void Exit(CameraController t)
        {
            base.Exit(t);
        }

        public float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;

            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}