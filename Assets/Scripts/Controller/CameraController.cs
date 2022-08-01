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
        public float minZoom = -10f;
        public float maxZoom = -1f;
        public float zoomSpeed = 1f;
        public float zoomSmoothSpeed = 0.125f;

        Vector3 velocity;
        Vector3 desiredPosition;
        Vector3 desiredRotation = default;
        Vector3 smoothedPosition;

        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>().ship.transform;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;


        }

        // Update is called once per frame
        void Update()
        {
            Quaternion camAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up);
            Quaternion camAngleY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotateSpeed, Vector3.right);
            
            cameraOffset = camAngleX * camAngleY * cameraOffset;
            
            cameraOffset = new Vector3(cameraOffset.x, Mathf.Clamp(cameraOffset.y, 0.5f, 4), cameraOffset.z);

            desiredPosition = target.position + cameraOffset + desiredRotation;
            smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target.position + targetOffset);
        }
    }
}