using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Data
{
    public class ProjectileData : ScriptableObject
    {
        [Header("Projectile Data")]
        [Tooltip("������")]
        public int damage;  //Default Damage
        //[Tooltip("��ź �ּ� ����")]
        //public float ricochetMin = 45.0f;  // Ricochet Minimum Angle
        //[Tooltip("��ź �ִ� ����")]
        //public float ricochetMax = 60.0f;  // Ricochet Maximum Angle
        [Tooltip("ȭ�� �ο� Ȯ��")]
        public float burnProbability = 0.0f;   // Percent chance of explosion
        //[Tooltip("�����")]
        //public float penetratingPower = 1;  // power of penetrating bullet
        //[Tooltip("��� Ȯ��")]
        //public float spreadProbability = 1;  // power of penetrating bullet
    }
}