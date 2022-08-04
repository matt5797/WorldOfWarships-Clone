using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Data;
using UnityEngine.Events;
using System;

namespace WOW.DamageSystem
{
    public class HP : MonoBehaviour
    {
        public int hp = 100;
        public UnityEvent onDead;
        public UnityEvent<DamageInfo> onHit, onThrough; // bulletID,  damage
        public float calcInterval;
        Dictionary<int, List<DamageInfo>> damageInfoDict = new Dictionary<int, List<DamageInfo>>();
        bool isDead = false;

        private void Start()
        {
            //onHit = new UnityEvent<DamageInfo>();
            onThrough = new UnityEvent<DamageInfo>();
            //onHit.AddListener(OnHit);
            onThrough.AddListener(OnThrough);

            Damageable[] damageables = GetComponentsInParent<Damageable>();
            foreach (Damageable damageable in damageables)
            {
                damageable.onHit.AddListener(OnHit);
                damageable.onThrough.AddListener(OnThrough);
            }
        }

        private void OnThrough(DamageInfo damageInfo)
        {
            if (damageInfoDict.ContainsKey(damageInfo.bulletID))
            {
                damageInfoDict[damageInfo.bulletID].Add(damageInfo);
            }
        }

        private void OnHit(DamageInfo damageInfo)
        {
            if (damageInfoDict.ContainsKey(damageInfo.bulletID))
            {
                damageInfoDict[damageInfo.bulletID].Add(damageInfo);
            }
            else
            {
                damageInfoDict.Add(damageInfo.bulletID, new List<DamageInfo>() { damageInfo });
            }
        }

        IEnumerator WaitForApplyDamage()
        {
            yield return new WaitForSeconds(calcInterval);
            ApplyDamage();
        }

        private void ApplyDamage()
        {
            foreach (KeyValuePair<int, List<DamageInfo>> pair in damageInfoDict)
            {
                int damage = 0;
                // 과관통
                if (pair.Value.Count % 2 == 0)
                {
                    for (int i = 0; i < pair.Value.Count; i++)
                    {
                        damage += pair.Value[i].damage / 10;
                    }
                }
                // 과관통 안함
                else
                {
                    for (int i = 0; i < pair.Value.Count; i++)
                    {
                        damage += pair.Value[i].damage;
                    }
                    damage += pair.Value[pair.Value.Count].damage;
                }
                
                hp -= damage;
                if (hp <= 0)
                {
                    isDead = true;
                    onDead.Invoke();
                    break;
                }
            }
            damageInfoDict.Clear();
        }
    }
}