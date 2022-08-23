using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Data;
using UnityEngine.Events;
using System;
using WOW.UI;

namespace WOW.DamageSystem
{
    public class DamageableManager : MonoBehaviour
    {
        public int maxHP = 100;
        int runtimeHP;
        public UnityEvent onDead;
        public UnityEvent onApplyDamage;
        public UnityEvent<DamageInfo> onHit, onThrough;
        public float calcInterval;
        Dictionary<int, List<DamageInfo>> damageInfoDict = new Dictionary<int, List<DamageInfo>>();
        bool isDead = false;
        Damageable[] damageables;
        public HPBar hpBar;
        public bool isImmutable = false;
        public int HP
        {
            get { return runtimeHP; }
            set 
            {
                runtimeHP = value;
                hpBar.SetHPPercent((float)runtimeHP / (float)maxHP);
            }
        }

        private void Start()
        {
            runtimeHP = maxHP;
            //hpBar = GetComponentInChildren<HPBar>();
            onThrough = new UnityEvent<DamageInfo>();
            onThrough.AddListener(OnThrough);

            damageables = GetComponentsInChildren<Damageable>();
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
                StartCoroutine("WaitForApplyDamage", damageInfo.bulletID);
                damageInfoDict.Add(damageInfo.bulletID, new List<DamageInfo>() { damageInfo });
            }
        }

        IEnumerator WaitForApplyDamage(int bulletID)
        {
            yield return new WaitForSeconds(calcInterval);
            if (damageInfoDict.ContainsKey(bulletID))
            {
                ApplyDamage(bulletID);
            }
            yield break;
        }

        public void ApplyDamage(int bulletID)
        {
            //print("ApplyDamage");
            if (!damageInfoDict.ContainsKey(bulletID))
                return;
            List<DamageInfo> damageInfos = damageInfoDict[bulletID];
            Stack<int> stack = new Stack<int>();

            int damage = 0;

            foreach (DamageInfo damageInfo in damageInfos)
            {
                //Debug.LogAssertion(damageInfo.hitPointID +" "+damageInfo.damage);
                if (damageInfo.damage == 0)
                {
                    if (stack.Peek()==damageInfo.hitPointID)                    
                        stack.Pop();
                }
                else
                {
                    stack.Push(damageInfo.hitPointID);
                }
            }

            // 과관통
            if (stack.Count == 0)
            {
                DamageTextManager.Instance.CreateDamageText(transform, "과관통", 20);
                for (int i = 0; i < damageInfos.Count; i++)
                {
                    if (damageInfos[i].damage>0)
                    {
                        //damage += damageInfos[i].damage / 10;
                        damage += damageInfos[i].damageable.ApplyDamage(damageInfos[i].damage / 10);
                    }
                }
            }
            // 과관통 안함
            else
            {
                DamageTextManager.Instance.CreateDamageText(transform, "관통", 20);
                for (int i = 0; i < damageInfos.Count; i++)
                {
                    if (damageInfos[i].damage > 0)
                    {
                        //damage += damageInfos[i].damage / 3;
                        damage += damageInfos[i].damageable.ApplyDamage(damageInfos[i].damage / 3);
                    }
                }
            }

            //print(damage);
            HP -= damage;
            if (HP <= 0)
            {
                HP = 0;
                if (!isImmutable)
                {
                    onDead.Invoke();
                    isDead = true;
                }
            }
            else
            {
                DamageTextManager.Instance.CreateDamageText(transform, damage.ToString(), 20);
            }

            damageInfoDict.Remove(bulletID);
        }
    }
}