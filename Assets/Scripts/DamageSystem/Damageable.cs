using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WOW.Data;

namespace WOW.DamageSystem
{
    public abstract class Damageable : MonoBehaviour
    {
        DamageableData damageableData;
        UnityEvent onBreakdown, onRecovery, onCompleteDestroy, onFire;
        bool canDamage = true;
        float destroyTime;
        Damageable[] neighbors;

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

        public void CheckEffect(ProjectileData projectileData, float incidenceAngle)
        {
            CheckFire(projectileData.burnProbability);
            CheckSpread(projectileData.spreadProbability, projectileData.damage);
            ApplyDamage(projectileData.damage, incidenceAngle);
        }

        public void CheckFire(float burnProbability)
        {
            if (burnProbability > damageableData.res_fire)
            {
                onFire.Invoke();
            }
        }

        public void CheckSpread(float spreadProbability, float damage)
        {
            if (UnityEngine.Random.Range(0, 100+ damageableData.res_spread) > spreadProbability)
            {
                foreach(Damageable neighbor in neighbors)
                {
                    neighbor.ReceiveSpread((int)damage/10);
                }
            }
        }

        public void ReceiveSpread(int spreadDamage)
        {
            damageableData.runtimeHP -= spreadDamage;
        }

        public void ApplyDamage(float damage, float incidenceAngle)
        {
            if (canDamage)
            {
                // (damageableData.runtimeHP, (int)(damage * damageableData.multiple));
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