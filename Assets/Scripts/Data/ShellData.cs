using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Data
{
    
    [CreateAssetMenu(fileName = "ShellData", menuName = "Scriptable Object/Shell Data", order = 0)]
    public class ShellData : ProjectileData
    {
        [Header("Shell Data")]
        [Tooltip("��ź ����")]
        public int W = 55; // SHELL WEIGHT
        [Tooltip("��ź ����")]
        public float D = 0.152f; // SHELL DIAMETER
        [Tooltip("��ź �׷�")]
        public float c_D = 0.321f;  // SHELL DRAG
        [Tooltip("��ź �߻� �ӵ�")]
        public int V_0 = 950; // SHELL MUZZLE VELOCITY
        [Tooltip("��ź KRUPP")]
        public int K = 2216; // SHELL KRUPP
    }
}