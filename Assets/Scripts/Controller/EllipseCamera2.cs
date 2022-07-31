using UnityEngine;

namespace WOW.Controller
{
    [RequireComponent(typeof(Camera))]
    public class EllipseCamera2 : MonoBehaviour
    {
        public Transform m_Target;
        public float m_Distance = 5.0f;
        public float m_XSpeed = 120.0f;
        public float m_YSpeed = 120.0f;

        public float m_YMinLimit = -20f;
        public float m_YMaxLimit = 80f;

        public float m_DistanceMin = .5f;
        public float m_DistanceMax = 15f;

        private float m_X = 0.0f;
        private float m_Y = 0.0f;

        public Vector3 targetOffset;
        public Vector3 targetOffsetMin;
        public Vector3 targetOffsetMax;
        public Vector3 targetOffset2;
        public Vector3 cameraOffset;
        public Vector3 cameraOffset2;
        public Vector3 cameraOffsetMin;
        public Vector3 cameraOffsetMax;

        public float targetOffsetYSpeed = 5;
        public float targetOffsetYMin = 0;
        public float targetOffsetYMax = 10;

        public float targetOffsetZSpeed = 5;
        public float targetOffsetZMin = 0;
        public float targetOffsetZMax = 10;

        //Ellipse cameraEllipse;
        //Ellipse targetEllipse;
        Cone cameraCone;
        Ellipsoid targetEllipsoid;

        public float targetEllipseXAxis = 1;
        public float targetEllipseYAxis = 10;
        public float targetEllipseZAxis = 5;

        public float cameraConeXAxis = 1;
        public float cameraConeYAxis = 10;
        public float cameraConeZAxis = 5;
        public float cameraConeYOffset = 1;

        float t;
        float u;

        public float rotateSpeed = 0.125f;
        public float zoomLevel = default;

        public float zoomSpeed = 15f;

        void Start()
        {
            Vector3 angles = transform.eulerAngles;
            m_X = angles.y;
            m_Y = angles.x;

            //targetEllipse = new Ellipse(targetEllipseXAxis, targetEllipseYAxis);
            //cameraEllipse = new Ellipse(cameraEllipseXAxis, cameraEllipseYAxis);

            targetEllipsoid = new Ellipsoid(targetEllipseXAxis, targetEllipseYAxis, targetEllipseZAxis, targetOffset2.x, targetOffset2.y, targetOffset2.z);
            //cameraCone = new Cone(cameraConeXAxis, cameraConeYAxis, cameraConeZAxis, cameraConeYOffset);
        }

        void LateUpdate()
        {
            if (m_Target)
            {
                m_Y -= Input.GetAxis("Mouse Y") * m_YSpeed * 0.02f;

                m_Y = ClampAngle(m_Y, m_YMinLimit, m_YMaxLimit);

                t += (Input.GetAxis("Mouse X") * targetOffsetZSpeed);
                u = Mathf.Clamp(u - (Input.GetAxis("Mouse ScrollWheel") * targetOffsetYSpeed), 0, targetOffsetMax.y);

                targetOffset = GetTargetOffset(t, u);

                float distance = Vector3.Distance(m_Target.position + targetOffset, transform.position + cameraOffset);
                m_Distance = Mathf.Clamp(m_Distance - Input.GetAxis("Mouse ScrollWheel") * distance * zoomSpeed, m_DistanceMin, m_DistanceMax);

                cameraOffset = targetOffset.normalized * m_Distance;
                //cameraOffset = new Vector3(targetOffset.normalized.z * m_Distance, 0, targetOffset.normalized.z * m_Distance);
                Vector3 position = m_Target.position + targetOffset;
                transform.position = position + cameraOffset;

                //transform.position = new Vector3(transform.position.x, position.y, transform.position.z);
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraOffsetMin.x, cameraOffsetMax.x),
                    Mathf.Clamp(position.y + cameraOffset2.y, cameraOffsetMin.y, targetOffsetMax.y),
                    Mathf.Clamp(transform.position.z, cameraOffsetMin.z, cameraOffsetMax.z));

                Quaternion rotation = Quaternion.LookRotation(position - transform.position);
                rotation *= Quaternion.Euler(m_Y, 0, 0);
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

        Vector3 GetCameraOffset(float t, float u)
        {
            return cameraCone.Evaluate(t, u);
        }

        /*Vector3 GetTargetOffset(float t, float y)
        {
            var res = targetEllipse.Evaluate(t);
            return new Vector3(res.x, y, res.y);
        }
        
        Vector3 GetCameraOffset(float t, float y)
        {
            var res = cameraEllipse.Evaluate(t);
            return new Vector3(res.x, y, res.y);
        }*/
    }
}
