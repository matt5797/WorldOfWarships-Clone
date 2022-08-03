using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Data
{
    
    [CreateAssetMenu(fileName = "ShellData", menuName = "Scriptable Object/Shell Data", order = 0)]
    public class ShellData : ProjectileData
    {
        [Header("Shell Data")]
        [Tooltip("Æ÷Åº ¹«°Ô")]
        public int W = 55; // SHELL WEIGHT
        [Tooltip("Æ÷Åº ±¸°æ")]
        public float D = 0.152f; // SHELL DIAMETER
        [Tooltip("Æ÷Åº Ç×·Â")]
        public float c_D = 0.321f;  // SHELL DRAG
        [Tooltip("Æ÷Åº ¹ß»ç ¼Óµµ")]
        public int V_0 = 950; // SHELL MUZZLE VELOCITY
        [Tooltip("Æ÷Åº KRUPP")]
        public int K = 2216; // SHELL KRUPP
    }
}