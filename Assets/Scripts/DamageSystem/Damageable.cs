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
        public DamageableData damageableData;
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

        public bool CheckOvermatch(float diameter)
        {
            if (diameter > damageableData.armor * 14.3)
            {
                return true;
            }
            return false;
        }

        // check ricochet
        public bool CheckRicochet(float angle, float ricochetAt, float alwaysRicochetAt)
        {
            if (angle > alwaysRicochetAt)
            {
                //print("¹«Á¶°Ç µµÅº: ");
                return true;
            }
            if (angle > ricochetAt && Random.Range(1, 100) < 50)
            {
                //print("·£´ý µµÅº: ");
                return true;
            }
            return false;
        }
        
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
            if (Random.Range(1,100) + damageableData.res_fire < burnProbability)
            {
                onFire.Invoke();
            }
        }


        public void ReceiveSpread(int spreadDamage)
        {
            damageableData.runtimeHP -= spreadDamage;
        }

        public void CheckDamage(int damage, int bulletID)
        {
            //print("CheckDamage");
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

        public void ApplyDamage(int damage)
        {
            print("ApplyDamage");
        }


        private void OnRecovery()
        {
            DamageTextManager.Instance.CreateDamageText(transform, "È¸º¹", 12);
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