using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Data;

namespace WOW.Controller
{
    public class EllipsoidState : State<CameraController>
    {
        Ellipsoid targetEllipsoid;
        private float m_Y = 0.0f;
        float u = default;
        float v = default;

        public override State<CameraController> InputHandle(CameraController t)
        {
            if (t.stateData != null && t.stateData.targetOffset.y >= t.stateData.stateChangeYMax)
            {
                return new OrbitState();
            }
            return this;
        }

        public override void Enter(CameraController t)
        {
            base.Enter(t);
            t.stateData = Resources.Load<CameraStateData>("Datas/CameraState/EllipsoidState");
            Vector3 angles = t.transform.eulerAngles;
            m_Y = angles.x;

            targetEllipsoid = new Ellipsoid(t.stateData.targetEllipseXAxis, t.stateData.targetEllipseYAxis, t.stateData.targetEllipseZAxis);
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
                m_Y -= Input.GetAxis("Mouse Y") * t.stateData.m_YSpeed * 0.02f;

                m_Y = ClampAngle(m_Y, t.stateData.m_YMinLimit, t.stateData.m_YMaxLimit);

                u += (Input.GetAxis("Mouse X") * t.stateData.targetOffsetZSpeed);
                v = Mathf.Clamp(v - (Input.GetAxis("Mouse ScrollWheel") * t.stateData.targetOffsetYSpeed), 0, t.stateData.targetOffsetYMax);

                if (targetEllipsoid != null)
                    t.stateData.targetOffset = GetTargetOffset(u, v);

                float distance = Vector3.Distance(t.m_Target.position + t.stateData.targetOffset, t.transform.position + t.stateData.cameraOffset);
                t.stateData.m_Distance = Mathf.Clamp(t.stateData.m_Distance - Input.GetAxis("Mouse ScrollWheel") * distance, t.stateData.m_DistanceMin, t.stateData.m_DistanceMax);

                t.stateData.cameraOffset = t.stateData.targetOffset.normalized * t.stateData.m_Distance;
                Vector3 position = t.m_Target.position + t.stateData.targetOffset;
                t.transform.position = position + t.stateData.cameraOffset;

                t.transform.position = new Vector3(t.transform.position.x + t.stateData.cameraOffset.x,
                    position.y + t.stateData.cameraOffset2.y,
                    t.transform.position.z + t.stateData.cameraOffset.z);

                Quaternion rotation = Quaternion.LookRotation(position - t.transform.position);
                t.transform.rotation = rotation;
            }
        }

        public override void Exit(CameraController t)
        {
            base.Exit(t);
        }

        Vector3 GetTargetOffset(float t, float u)
        {
            return targetEllipsoid.Evaluate(t, u);
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