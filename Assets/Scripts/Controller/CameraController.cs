using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Controller;

namespace WOW.Controller
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;

        public float smoothSpeed = 0.125f;
        public float rotateSpeed = 0.125f;
        public Vector3 targetOffset;
        public Vector3 cameraOffset;
        public Vector3 targetOffsetStart;
        public Vector3 cameraOffsetStart;
        public float zoomLevel = default;
        public float minZoom = -1f;
        public float maxZoom = 2f;
        public float zoomSpeed = 1f;
        public float zoomSmoothSpeed = 0.125f;
        float zoomPosition = default;

        Vector3 velocity;
        Vector3 desiredPosition;
        Vector3 desiredRotation = default;
        Vector3 smoothedPosition;

        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>().ship.transform;
            targetOffsetStart = targetOffset;
            cameraOffsetStart = cameraOffset;
        }

        // Update is called once per frame
        void Update()
        {
            float wheelAxis = Input.GetAxis("Mouse ScrollWheel");
            zoomLevel = zoomLevel + (wheelAxis * zoomSpeed * Time.deltaTime);
            zoomLevel = Mathf.Clamp(zoomLevel, minZoom, maxZoom);

            Quaternion camAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up);
            Quaternion camAngleY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotateSpeed, Vector3.right);

            //print(camAngleY);
            cameraOffset = cameraOffset + (transform.forward * zoomLevel);
            cameraOffset = camAngleX * camAngleY * cameraOffset;
            print(camAngleX * camAngleY * cameraOffset);
            //cameraOffset = camAngleX * cameraOffset;
            cameraOffset = new Vector3(cameraOffset.x, Mathf.Clamp(cameraOffset.y, 0.5f, 4), cameraOffset.z);

            //targetOffset.y = Mathf.Clamp(targetOffset.y + (Input.GetAxis("Mouse Y") * rotateSpeed), 2, 4);
            //cameraOffset.y = Mathf.Clamp(cameraOffset.y + (Input.GetAxis("Mouse Y") * rotateSpeed), 4, 6);
            //print((targetOffset.y - targetOffsetStart.y));

            desiredPosition = target.position + cameraOffset + desiredRotation;
            smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target.position + targetOffset);
        }

        private void OnDrawGizmos()
        {
            //Gizmos.color = Color.red;
            //Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }
}