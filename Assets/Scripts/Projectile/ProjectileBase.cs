using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.DamageSystem;
using UnityEngine.Events;

namespace WOW.Projectile
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        protected DamageableManager targetDamageableManager;

        protected virtual void OnTriggerEnter(Collider other)
        {
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                OnImpact(damageable);
            }

            DamageableManager damageableManager = other.GetComponent<DamageableManager>();
            if (damageableManager != null)
            {
                targetDamageableManager = damageableManager;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                OnThrough(damageable);
            }

            DamageableManager damageableManager = other.GetComponent<DamageableManager>();
            if (damageableManager != null)
            {
                // 과관통한 경우 데미지 정산
                //OnApplyDamage();
            }
        }

        protected abstract void OnImpact(Damageable damageable);
        protected abstract void OnThrough(Damageable damageable);
        protected void OnApplyDamage()
        {
            print("데미지 정산");
            targetDamageableManager.ApplyDamage(GetInstanceID());
        }

        //protected abstract void OnShoot();
    }
}
