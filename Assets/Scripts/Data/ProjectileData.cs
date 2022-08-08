using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Data
{
    public class ProjectileData : ScriptableObject
    {
        [Header("Projectile Data")]
        [Tooltip("데미지")]
        public int damage;  //Default Damage
        //[Tooltip("도탄 최소 각도")]
        //public float ricochetMin = 45.0f;  // Ricochet Minimum Angle
        //[Tooltip("도탄 최대 각도")]
        //public float ricochetMax = 60.0f;  // Ricochet Maximum Angle
        [Tooltip("화염 부여 확률")]
        public float burnProbability = 0.0f;   // Percent chance of explosion
        //[Tooltip("관통력")]
        //public float penetratingPower = 1;  // power of penetrating bullet
        //[Tooltip("방사 확률")]
        //public float spreadProbability = 1;  // power of penetrating bullet
    }
}