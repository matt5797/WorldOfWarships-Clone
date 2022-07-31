using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (t.targetOffset.y >= t.stateChangeY)
            {
                return new OrbitState();
            }
            return this;
        }

        public override void Enter(CameraController t)
        {
            base.Enter(t);
            Vector3 angles = t.transform.eulerAngles;
            m_Y = angles.x;

            targetEllipsoid = new Ellipsoid(t.targetEllipseXAxis, t.targetEllipseYAxis, t.targetEllipseZAxis);

            Debug.Log(t.transform.position);
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
                m_Y -= Input.GetAxis("Mouse Y") * t.m_YSpeed * 0.02f;

                m_Y = ClampAngle(m_Y, t.m_YMinLimit, t.m_YMaxLimit);

                u += (Input.GetAxis("Mouse X") * t.targetOffsetZSpeed);
                v = Mathf.Clamp(v - (Input.GetAxis("Mouse ScrollWheel") * t.targetOffsetYSpeed), 0, t.targetOffsetYMax);

                if (targetEllipsoid != null)
                    t.targetOffset = GetTargetOffset(u, v);

                float distance = Vector3.Distance(t.m_Target.position + t.targetOffset, t.transform.position + t.cameraOffset);
                t.m_Distance = Mathf.Clamp(t.m_Distance - Input.GetAxis("Mouse ScrollWheel") * distance, t.m_DistanceMin, t.m_DistanceMax);

                t.cameraOffset = t.targetOffset.normalized * t.m_Distance;
                Vector3 position = t.m_Target.position + t.targetOffset;
                t.transform.position = position + t.cameraOffset;

                t.transform.position = new Vector3(t.transform.position.x + t.cameraOffset.x,
                    position.y + t.cameraOffset2.y,
                    t.transform.position.z + t.cameraOffset.z);

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