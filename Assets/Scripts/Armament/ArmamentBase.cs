using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Armament
{
    public abstract class ArmamentBase : MonoBehaviour
    {
        public float cooldownTime = 1;
        private bool canUse = true;
        
        public virtual bool TryFire()
        {
            if (CanFire())
            {
                Fire();
                StartCooldown();
                return true;
            }
            return false;
        }

        protected virtual bool CanFire()
        {
            return true;
        }

        protected abstract void Fire();

        void StartCooldown()
        {
            StartCoroutine(Cooldown());
            IEnumerator Cooldown()
            {
                canUse = false;
                yield return new WaitForSeconds(cooldownTime);
                canUse = true;
            }
        }
    }
}