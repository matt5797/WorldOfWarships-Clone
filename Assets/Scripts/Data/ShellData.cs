using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Data
{
    [CreateAssetMenu(fileName = "ShellData", menuName = "Scriptable Object/Shell Data", order = 0)]
    public class ShellData : ProjectileData
    {
        [Header("Shell Data")]
        public int W = 55; // SHELL WEIGHT
        public float D = 0.152f; // SHELL DIAMETER
        public float c_D = 0.321f;  // SHELL DRAG
        public int V_0 = 950; // SHELL MUZZLE VELOCITY
        public int K = 2216; // SHELL KRUPP
        public float ricochetMin = 45.0f;  // Ricochet Minimum Angle
        public float ricochetMax = 60.0f;  // Ricochet Maximum Angle
        public float burnProbability = 30.0f;   // Percent chance of explosion
        public float penetratingPower = 1;  // power of penetrating bullet
    }
}