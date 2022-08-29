using UnityEngine;

namespace WOW.Controller
{
    [RequireComponent(typeof(Camera))]
    public class ModelCamera : MonoBehaviour
    {
        public Transform m_Target;
        public float m_Distance = 5.0f;
        public float m_YSpeed = 120.0f;

        public float m_YMinLimit = -20f;
        public float m_YMaxLimit = 80f;

        public float m_DistanceMin = .5f;
        public float m_DistanceMax = 15f;

        private float m_Y = 0.0f;

        public Vector3 targetOffset;
        public Vector3 cameraOffset;

        public float targetOffsetYSpeed = 5;
        public float targetOffsetYMin = 0;
        public float targetOffsetYMax = 10;

        public float targetOffsetZSpeed = 5;

        Ellipsoid targetEllipsoid;

        public float targetEllipseXAxis = 1;
        public float targetEllipseYAxis = 10;
        public float targetEllipseZAxis = 5;

        float u;
        float v = default;

        public Vector3 cameraOffset2;

        public Vector3 originPos;
        public Quaternion originRot;
        
        enum CameraState
        {
            Origin,
            Model
        }
        CameraState state = CameraState.Origin;

        void Start()
        {
            originPos = transform.position;
            originRot = transform.rotation;

            Vector3 angles = transform.eulerAngles;
            m_Y = angles.x;

            targetEllipsoid = new Ellipsoid(targetEllipseXAxis, targetEllipseYAxis, targetEllipseZAxis);
        }

        void LateUpdate()
        {
            if (state == CameraState.Origin && Input.GetKeyDown(KeyCode.Alpha2))
            {
                state = CameraState.Model;
            }
            if (state == CameraState.Model && Input.GetKeyDown(KeyCode.Alpha1))
            {
                state = CameraState.Origin;
                transform.position = originPos;
                transform.rotation = originRot;
            }
            if (m_Target && state==CameraState.Model)
            {
                m_Y -= Input.GetAxis("Mouse Y") * m_YSpeed * 0.02f;

                m_Y = ClampAngle(m_Y, m_YMinLimit, m_YMaxLimit);

                u += (Input.GetAxis("Mouse X") * targetOffsetZSpeed);
                v = Mathf.Clamp(v - (Input.GetAxis("Mouse ScrollWheel") * targetOffsetYSpeed), targetOffsetYMin, targetOffsetYMax);

                if (targetEllipsoid != null)
                    targetOffset = GetTargetOffset(u, v);

                float distance = Vector3.Distance(m_Target.position + targetOffset, transform.position + cameraOffset);
                m_Distance = Mathf.Clamp(m_Distance - Input.GetAxis("Mouse ScrollWheel") * distance, m_DistanceMin, m_DistanceMax);

                cameraOffset = targetOffset.normalized * m_Distance;
                Vector3 position = m_Target.position + targetOffset;
                transform.position = position + cameraOffset;

                transform.position = new Vector3(transform.position.x + cameraOffset.x,
                    position.y + cameraOffset2.y,
                    transform.position.z + cameraOffset.z);

                Quaternion rotation = Quaternion.LookRotation(position - transform.position);
                transform.rotation = rotation;
            }
        }

        public float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;

            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }

        public float ClampAngleDirection(float angle, float min, float max)
        {
            return Mathf.Clamp(angle, min, max);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(m_Target.position + targetOffset, 1f);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 1f);
        }

        Vector3 GetTargetOffset(float t, float u)
        {
            return targetEllipsoid.Evaluate(t, u);
        }
    }
}
