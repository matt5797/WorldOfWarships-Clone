using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Data
{
    [CreateAssetMenu(fileName = "CameraStateData", menuName = "Scriptable Object/Camera State Data", order = 0)]
    public class CameraStateData : ScriptableObject, ISerializationCallbackReceiver
    {
        [Header("Camera State Data")]
        public float m_Distance = 5.0f;
        public float m_XSpeed = 120.0f;
        public float m_YSpeed = 120.0f;

        public float m_YMinLimit = -20f;
        public float m_YMaxLimit = 80f;

        public float m_DistanceMin = .5f;
        public float m_DistanceMax = 15f;

        public Vector3 targetOffset;
        public Vector3 targetOffset2;
        public Vector3 cameraOffset;
        public Vector3 cameraOffset2;

        public float targetOffsetYSpeed = 5;
        public float targetOffsetYMin = 0;
        public float targetOffsetYMax = 10;

        public float targetOffsetZSpeed = 5;
        public float targetOffsetZMin = 0;
        public float targetOffsetZMax = 10;

        public float targetEllipseXAxis = 1;
        public float targetEllipseYAxis = 10;
        public float targetEllipseZAxis = 5;

        public float stateChangeYMin = 1;
        public float stateChangeYMax = 5;

        //RunTime
        [NonSerialized] public Vector3 runtimeTargetOffset;
        [NonSerialized] public Vector3 runtimeTargetOffset2;
        [NonSerialized] public Vector3 runtimeCameraOffset;
        [NonSerialized] public Vector3 runtimeCameraOffset2;

        public void OnAfterDeserialize()
        {
            
        }

        public void OnBeforeSerialize()
        {
            runtimeTargetOffset = targetOffset;
            runtimeTargetOffset2 = targetOffset2;
            runtimeCameraOffset = cameraOffset;
            runtimeCameraOffset2 = cameraOffset2;
        }
    }
}