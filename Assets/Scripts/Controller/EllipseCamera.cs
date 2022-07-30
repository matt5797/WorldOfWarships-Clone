using UnityEngine;

namespace WOW.Controller
{
    [RequireComponent(typeof(Camera))]
    public class EllipseCamera: MonoBehaviour
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
        public Vector3 cameraOffset;

        public float targetOffsetYSpeed = 5;
        public float targetOffsetYMin = 0;
        public float targetOffsetYMax = 10;

        public float targetOffsetZSpeed = 5;
        public float targetOffsetZMin = 0;
        public float targetOffsetZMax = 10;

        Ellipse cameraEllipse;
        Ellipse targetEllipse;
        public float targetEllipseXAxis = 1;
        public float targetEllipseYAxis = 10;

        public float cameraEllipseXAxis = 1;
        public float cameraEllipseYAxis = 10;

        float t;
        
        public float rotateSpeed = 0.125f;
        public float zoomLevel = default;

        void Start()
        {
            Vector3 angles = transform.eulerAngles;
            m_X = angles.y;
            m_Y = angles.x;

            targetEllipse = new Ellipse(targetEllipseXAxis, targetEllipseYAxis);
            cameraEllipse = new Ellipse(cameraEllipseXAxis, cameraEllipseYAxis);
        }

        void LateUpdate()
        {
            if (m_Target)
            {
                //targetOffsetY = Input.GetAxis("Mouse ScrollWheel");
                //targetOffset.y = targetOffset.y - (Input.GetAxis("Mouse ScrollWheel") * targetOffsetYSpeed);
                //targetOffset.y = Mathf.Clamp(targetOffset.y - (Input.GetAxis("Mouse ScrollWheel") * targetOffsetYSpeed), targetOffsetYMin, targetOffsetYMax);

                //targetOffset.z = Mathf.Clamp(targetOffset.z - (Input.GetAxis("Mouse X") * targetOffsetZSpeed), targetOffsetZMin, targetOffsetZMax);
                //targetOffset.z = ClampAngleDirection(targetOffset.z - (Input.GetAxis("Mouse X") * targetOffsetZSpeed), targetOffsetZMin, targetOffsetZMax);
                
                //Quaternion camAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up);
                Quaternion camAngleY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotateSpeed, Vector3.right);

                //print(camAngleY);
                //cameraOffset = cameraOffset + (transform.forward * zoomLevel);
                //cameraOffset = camAngleX * camAngleY * cameraOffset;
                cameraOffset = camAngleY * cameraOffset;

                t += (Input.GetAxis("Mouse X") * targetOffsetZSpeed);
                targetOffset = GetTargetOffset(t, targetOffset.y);

                cameraOffset = GetCameraOffset(t, cameraOffset.y);

                Vector3 negDistance = new Vector3(0.0f, 0.0f, -m_Distance);
                //Vector3 position = negDistance + m_Target.position + targetOffset;
                Vector3 position = m_Target.position + targetOffset;
                transform.position = position + cameraOffset;
            }
            if (m_Target)
            {
                m_X += Input.GetAxis("Mouse X") * m_XSpeed * m_Distance * 0.02f;
                m_Y -= Input.GetAxis("Mouse Y") * m_YSpeed * 0.02f;

                m_Y = ClampAngle(m_Y, m_YMinLimit, m_YMaxLimit);

                //Quaternion rotation = Quaternion.Euler(m_Y, m_X, 0);

                float distance = Vector3.Distance(m_Target.position + targetOffset, transform.position + cameraOffset);
                m_Distance = Mathf.Clamp(m_Distance - Input.GetAxis("Mouse ScrollWheel") * distance, m_DistanceMin, m_DistanceMax);

                //Vector3 negDistance = new Vector3(0.0f, 0.0f, -m_Distance);
                //Vector3 position = rotation * negDistance + m_Target.position + targetOffset;
                //Vector3 position = negDistance + m_Target.position + targetOffset;

                //transform.rotation = rotation;
                //transform.position = position + cameraOffset;                

                transform.LookAt(m_Target.position + targetOffset);
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

        Vector3 GetTargetOffset(float t, float y)
        {
            var res = targetEllipse.Evaluate(t);
            return new Vector3(res.x, y, res.y);
        }
        
        Vector3 GetCameraOffset(float t, float y)
        {
            var res = cameraEllipse.Evaluate(t);
            return new Vector3(res.x, y, res.y);
        }
    }

    public class Ellipse
    {
        public float xAxis;
        public float yAxis;

        public Ellipse(float xAxis, float yAxis)
        {
            this.xAxis = xAxis;
            this.yAxis = yAxis;
        }

        public Vector2 Evaluate(float t)
        {
            float angle = Mathf.Deg2Rad * 360f * t;
            float x = xAxis * Mathf.Sin(angle);
            float y = yAxis * Mathf.Cos(angle);
            return new Vector2(x, y);
        }
    }

}
