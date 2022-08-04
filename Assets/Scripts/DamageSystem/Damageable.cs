using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WOW.Data;
using WOW.BattleShip;

namespace WOW.DamageSystem
{
    public struct DamageInfo
    {
        public float time;
        public int damage;
        public int bulletID;
        public int hitPointID;
    }
    
    public abstract class Damageable : MonoBehaviour
    {
        DamageableData damageableData;
        public UnityEvent<DamageInfo> onHit, onThrough;
        public UnityEvent onBreakdown, onRecovery, onCompleteDestroy, onFire;
        bool canDamage = true;
        float destroyTime;

        private void Start()
        {
            onBreakdown.AddListener(OnBreakdown);
            onRecovery.AddListener(OnRecovery);
            onCompleteDestroy.AddListener(OnCompleteDestroy);
            onFire.AddListener(OnFire);
        }

        private void OnRecovery()
        {
            
        }

        // need check ricochet?
        public bool CheckPenetrate(ref float penetration)
        {
            penetration -= damageableData.res_penetrate;
            if (penetration > 0)
                return true;
            return false;
        }

        public void CheckEffect(ProjectileData projectileData)
        {
            CheckFire(projectileData.burnProbability);
        }

        public void CheckFire(float burnProbability)
        {
            if (burnProbability > damageableData.res_fire)
            {
                onFire.Invoke();
            }
        }

        public bool CheckOvermatch(float diameter)
        {
            if (diameter > damageableData.armor * 14.3)
                return true;
            return false;
        }

        public void ReceiveSpread(int spreadDamage)
        {
            damageableData.runtimeHP -= spreadDamage;
        }

        public void ApplyDamage(int damage, float incidenceAngle, int bulletID)
        {
            if (canDamage)
            {
                DamageInfo damageInfo = new DamageInfo();
                damageInfo.time = Time.time;
                damageInfo.damage = damage;
                damageInfo.bulletID = bulletID;
                damageInfo.hitPointID = GetInstanceID();
                onHit.Invoke(damageInfo);
            }
        }

        private void OnBreakdown()
        {
            destroyTime = Time.time;
            StartCoroutine(Recovery());
        }

        private void OnCompleteDestroy()
        {
            canDamage = false;
        }

        private void OnFire()
        {
            
        }

        private IEnumerator Recovery()
        {
            while (Time.time + destroyTime < damageableData.recoveryTime)
            {
                yield return new WaitForSeconds(0.1f);
            }
            onRecovery.Invoke();
            yield return null;
        }
    }
}