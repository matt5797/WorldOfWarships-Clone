using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Data;

namespace WOW.Controller
{
    public class OrbitState : State<CameraController>
    {
        private float m_X = 0.0f;
        private float m_Y = 0.0f;

        public override State<CameraController> InputHandle(CameraController t)
        {
            if (t.stateData != null && t.stateData.targetOffset.y <= t.stateData.stateChangeYMin)
            {
                return new EllipsoidState();
            }
            return this;
        }

        public override void Enter(CameraController t)
        {
            base.Enter(t);
            t.stateData = Resources.Load<CameraStateData>("Datas/CameraState/OrbitState");
            Debug.Log(t.stateData);
            Vector3 angles = t.transform.eulerAngles;
            m_X = angles.y;
            m_Y = angles.x;

            t.stateData.targetOffset = new Vector3(0, t.stateData.stateChangeYMin + 0.05f, 0);
            t.stateData.cameraOffset = Vector3.zero; //new Vector3(0, t.cameraOffset.y, 0);

            //t.stateData.m_XSpeed = 20;
        }

        public override void Update(CameraController t)
        {
            base.Update(t);
        }
        public override void LateUpdate(CameraController t)
        {
            base.LateUpdate(t);
            if (t.stateData != null && t.m_Target && !t.isChangingState)
            {
                t.stateData.targetOffset.y = Mathf.Clamp(t.stateData.targetOffset.y - (Input.GetAxis("Mouse ScrollWheel") * t.stateData.targetOffsetYSpeed), t.stateData.targetOffsetYMin, t.stateData.targetOffsetYMax);

                m_X += Input.GetAxis("Mouse X") * t.stateData.m_XSpeed * t.stateData.m_Distance * 0.02f;
                m_Y -= Input.GetAxis("Mouse Y") * t.stateData.m_YSpeed * 0.02f;

                m_Y = ClampAngle(m_Y, t.stateData.m_YMinLimit, t.stateData.m_YMaxLimit);

                Quaternion rotation = Quaternion.Euler(m_Y, m_X, 0);

                float distance = Vector3.Distance(t.m_Target.position + t.stateData.targetOffset, t.transform.position + t.stateData.cameraOffset);
                t.stateData.m_Distance = Mathf.Clamp(t.stateData.m_Distance - Input.GetAxis("Mouse ScrollWheel") * distance, t.stateData.m_DistanceMin, t.stateData.m_DistanceMax);

                Vector3 negDistance = new Vector3(0.0f, 0.0f, -t.stateData.m_Distance);
                Vector3 position = rotation * negDistance + t.m_Target.position + t.stateData.targetOffset;

                t.transform.rotation = rotation;
                t.transform.position = position + t.stateData.cameraOffset
                    + (t.transform.forward * t.stateData.cameraOffset2.z);
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