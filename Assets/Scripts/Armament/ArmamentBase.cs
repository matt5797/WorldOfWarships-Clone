using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Controller;

namespace WOW.Armament
{
    /// <summary>
    /// 무장 종류
    /// </summary>
    public enum ArmamentType
    {
        None,
        MainBatteryHE,
        MainBatteryAP,
        TorpedoTube,
    }
    
    // 모든 무장의 부모 클래스
    public abstract class ArmamentBase : MonoBehaviour
    {
        public float cooldownTime = 1;  // 쿨타임
        private bool canUse = true; // 사용 가능 여부
        
        public Transform rootPosition;
        [HideInInspector] public Vector3 target;
        public ShipController controller;
        public GameObject[] firePoint;
        public ParticleSystem[] muzzleFlashs;

        protected virtual void Start()
        {
            controller = GetComponentInParent<ShipController>();
        }

        /// <summary>
        /// 사격 가능하다면 사격하고, 사격에 성공했는지 반환
        /// </summary>
        /// <returns></returns>
        public virtual bool TryFire()
        {
            // 사격이 가능하다면
            if (CanFire())
            {
                // 사격
                Fire();
                // 쿨타임 기다린다.
                StartCooldown();
                return true;
            }
            // 사격 실패 반환
            return false;
        }

        /// <summary>
        /// 사격 가능한 상태인지 반환
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanFire()
        {
            return canUse;
        }

        /// <summary>
        /// 사격
        /// </summary>
        protected abstract void Fire();

        /// <summary>
        /// 쿨타임을 체크한다
        /// </summary>
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