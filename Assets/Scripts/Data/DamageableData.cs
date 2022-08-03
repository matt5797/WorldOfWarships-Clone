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
        [Tooltip("ȭ�� ����")]
        public float res_fire;
        [Tooltip("��ź ����")]
        public float res_ricochet;
        [Tooltip("���� ����")]
        public float res_penetrate;
        [Tooltip("��� ����")]
        public float res_spread;
        [Tooltip("���� �ð�")]
        public float recoveryTime;
        [Tooltip("������ ����")]
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