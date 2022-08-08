using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Data
{
    [CreateAssetMenu(fileName = "DamageableData", menuName = "Scriptable Object/Damageable Data", order = 0)]
    public class DamageableData : ScriptableObject, ISerializationCallbackReceiver
    {
        [Header("Damageable Data")]
        public int hp;
        [Tooltip("장갑 두께")]
        public float armor;
        [Tooltip("화염 저항")]
        public float res_fire;
        [Tooltip("도탄 저항")]
        public float res_ricochet;
        [Tooltip("관통 저항")]
        public float res_penetrate;
        [Tooltip("방사 저항")]
        public float res_spread;
        [Tooltip("수리 시간")]
        public float recoveryTime;
        [Tooltip("데미지 배율")]
        public float multiple;

        //RunTime
        [NonSerialized] public int runtimeHP;

        public void OnAfterDeserialize()
        {
            
        }

        public void OnBeforeSerialize()
        {
            runtimeHP = hp;
        }
    }
}