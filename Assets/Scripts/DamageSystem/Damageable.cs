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
        public Damageable damageable;
    }
    
    public abstract class Damageable : MonoBehaviour
    {
        public DamageableData damageableData;
        public UnityEvent<DamageInfo> onHit, onThrough;
        public UnityEvent onBreakdown, onRecovery, onCompleteDestroy, onFire;
        bool canDamage = true;
        public bool canRecovery = false;
        public float destroyTime = 5;

        [SerializeField] int hp;
        public int HP
        {
            get { return hp; }
            set
            {
                hp = value;
                if (hp <= 0)
                {
                    if (canRecovery)
                    {
                        onBreakdown.Invoke();
                    }
                    else
                    {
                        onCompleteDestroy.Invoke();
                    }
                    canDamage = false;
                }
            }
        }

        private void Start()
        {
            onBreakdown.AddListener(OnBreakdown);
            onRecovery.AddListener(OnRecovery);
            onCompleteDestroy.AddListener(OnCompleteDestroy);
            onFire.AddListener(OnFire);

            HP = damageableData.hp;
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
                //print("무조건 도탄: ");
                return true;
            }
            if (angle > ricochetAt && Random.Range(1, 100) < 50)
            {
                //print("랜덤 도탄: ");
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
            DamageInfo damageInfo = new DamageInfo();
            damageInfo.time = Time.time;
            damageInfo.damage = damage;
            damageInfo.bulletID = bulletID;
            damageInfo.hitPointID = GetInstanceID();
            damageInfo.damageable = this;
            onHit.Invoke(damageInfo);
        }

        public int ApplyDamage(int damage)
        {
            if (canDamage)
            {
                //print(damage + "/" + HP + " damage: " + gameObject);
                HP -= (int)(damage * damageableData.multiple);
                return (int)(damage * damageableData.multiple);
            }
            return 0;
        }


        private void OnRecovery()
        {
            canDamage = true;
            HP = damageableData.hp;
            DamageTextManager.Instance.CreateDamageText(transform, "회복", 12);
        }

        public void OnBreakdown()
        {
            StartCoroutine(Recovery());
            DamageTextManager.Instance.CreateDamageText(transform, "파손", 12);
        }

        public void OnCompleteDestroy()
        {
            canDamage = false;
            DamageTextManager.Instance.CreateDamageText(transform, "완전 파손", 12);
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

        public void OnThrough(int bulletID)
        {
            DamageInfo damageInfo = new DamageInfo();
            damageInfo.time = Time.time;
            damageInfo.damage = 0;
            damageInfo.bulletID = bulletID;
            damageInfo.hitPointID = GetInstanceID();
            damageInfo.damageable = this;
            onThrough.Invoke(damageInfo);
        }
    }
}