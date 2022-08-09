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
                //print(GetInstanceID() + " hit " + other.GetInstanceID());
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
        }

        protected abstract void OnImpact(Damageable damageable);
        protected abstract void OnThrough(Damageable damageable);
        protected void OnApplyDamage()
        {
            //print("데미지 정산");
            targetDamageableManager.ApplyDamage(GetInstanceID());
        }

        //protected abstract void OnShoot();
    }
}
