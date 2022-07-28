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
        public Vector3 targetOffset;
        public Vector3 cameraOffset;
        public float minZoom = -10f;
        public float maxZoom = -1f;
        public float zoomSpeed = 1f;
        public float zoomSmoothSpeed = 0.125f;

        Vector3 velocity;

        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>().ship.transform;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 desiredPosition = target.position + cameraOffset;
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target.position + targetOffset);
        }
    }
}