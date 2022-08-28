using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Controller;

namespace WOW.Armament
{
    /// <summary>
    /// ���� ����
    /// </summary>
    public enum ArmamentType
    {
        None,
        MainBatteryHE,
        MainBatteryAP,
        TorpedoTube,
    }
    
    // ��� ������ �θ� Ŭ����
    public abstract class ArmamentBase : MonoBehaviour
    {
        public float cooldownTime = 1;  // ��Ÿ��
        private bool canUse = true; // ��� ���� ����
        
        public Transform rootPosition;
        [HideInInspector] public Vector3 target;
        protected ShipController controller;
        public GameObject[] firePoint;
        public ParticleSystem[] muzzleFlashs;

        protected virtual void Start()
        {
            controller = GetComponentInParent<ShipController>();
        }

        /// <summary>
        /// ��� �����ϴٸ� ����ϰ�, ��ݿ� �����ߴ��� ��ȯ
        /// </summary>
        /// <returns></returns>
        public virtual bool TryFire()
        {
            // ����� �����ϴٸ�
            if (CanFire())
            {
                // ���
                Fire();
                // ��Ÿ�� ��ٸ���.
                StartCooldown();
                return true;
            }
            // ��� ���� ��ȯ
            return false;
        }

        /// <summary>
        /// ��� ������ �������� ��ȯ
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanFire()
        {
            return canUse;
        }

        /// <summary>
        /// ���
        /// </summary>
        protected abstract void Fire();

        /// <summary>
        /// ��Ÿ���� üũ�Ѵ�
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