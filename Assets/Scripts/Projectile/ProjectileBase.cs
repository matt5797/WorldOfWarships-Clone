using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.DamageSystem;

namespace WOW.Projectile
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        protected virtual void OnTriggerEnter(Collider other)
        {
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                OnImpact();
            }
        }

        protected abstract void OnImpact();
    }
}
