using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.DamageSystem;
using UnityEngine.Events;
using WOW.BattleShip;

namespace WOW.Projectile
{
    /// <summary>
    /// 모든 발사체의 부모 클래스
    /// </summary>
    public abstract class ProjectileBase : MonoBehaviour
    {
        protected DamageableManager targetDamageableManager;    // 대상 배의 모듈 관리자를 저장
        [HideInInspector] public Camp camp;   //내 진영

        protected virtual void OnTriggerEnter(Collider other)
        {
            BattleShipBase ship = other.GetComponentInParent<BattleShipBase>();
            if (ship == null)
                return;

            if (ship.camp == camp)
                return;
            
            // 충돌 대상이 Damageable 컴포넌트를 가지고 있는지 확인
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                //print(GetInstanceID() + " hit " + other.GetInstanceID());
                // 가지고 있다면 자식이 구현한 OnImpact 함수를 호출해준다.
                OnImpact(damageable);
            }

            // 충돌 대상이 DamageableManager 컴포넌트를 가지고 있는지 확인
            DamageableManager damageableManager = other.GetComponent<DamageableManager>();
            if (damageableManager != null)
            {
                // 가지고 있다면 해당 배의 모듈 관리자로 판단하고 저장해둔다.
                targetDamageableManager = damageableManager;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            BattleShipBase ship = other.GetComponentInParent<BattleShipBase>();
            if (ship == null)
                return;

            if (ship.camp == camp)
                return;
            
            // 충돌 대상이 Damageable 컴포넌트를 가지고 있는지 확인
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                // 가지고 있다면 자식이 구현한 OnThrough 함수를 호출해준다.
                OnThrough(damageable);
            }
        }

        /// <summary>
        /// 탄환이 모듈에 맞았을 때 호출되는 함수
        /// </summary>
        /// <param name="damageable">피격된 모듈</param>
        protected abstract void OnImpact(Damageable damageable);
        /// <summary>
        /// 탄환이 모듈을 관통했을 때 호출되는 함수
        /// </summary>
        /// <param name="damageable"></param>
        protected abstract void OnThrough(Damageable damageable);
        /// <summary>
        /// 누적된 데미지들에 대한 정산을 요청한다.
        /// </summary>
        protected void OnApplyDamage()
        {
            //print("데미지 정산");
            if (targetDamageableManager!=null)
                targetDamageableManager.ApplyDamage(GetInstanceID());
        }

        //protected abstract void OnShoot();
    }
}
