using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.DamageSystem;
using UnityEngine.Events;
using WOW.BattleShip;

namespace WOW.Projectile
{
    /// <summary>
    /// ��� �߻�ü�� �θ� Ŭ����
    /// </summary>
    public abstract class ProjectileBase : MonoBehaviour
    {
        protected DamageableManager targetDamageableManager;    // ��� ���� ��� �����ڸ� ����
        [HideInInspector] public Camp camp;   //�� ����

        protected virtual void OnTriggerEnter(Collider other)
        {
            BattleShipBase ship = other.GetComponentInParent<BattleShipBase>();
            if (ship == null)
                return;

            if (ship.camp == camp)
                return;
            
            // �浹 ����� Damageable ������Ʈ�� ������ �ִ��� Ȯ��
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                //print(GetInstanceID() + " hit " + other.GetInstanceID());
                // ������ �ִٸ� �ڽ��� ������ OnImpact �Լ��� ȣ�����ش�.
                OnImpact(damageable);
            }

            // �浹 ����� DamageableManager ������Ʈ�� ������ �ִ��� Ȯ��
            DamageableManager damageableManager = other.GetComponent<DamageableManager>();
            if (damageableManager != null)
            {
                // ������ �ִٸ� �ش� ���� ��� �����ڷ� �Ǵ��ϰ� �����صд�.
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
            
            // �浹 ����� Damageable ������Ʈ�� ������ �ִ��� Ȯ��
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                // ������ �ִٸ� �ڽ��� ������ OnThrough �Լ��� ȣ�����ش�.
                OnThrough(damageable);
            }
        }

        /// <summary>
        /// źȯ�� ��⿡ �¾��� �� ȣ��Ǵ� �Լ�
        /// </summary>
        /// <param name="damageable">�ǰݵ� ���</param>
        protected abstract void OnImpact(Damageable damageable);
        /// <summary>
        /// źȯ�� ����� �������� �� ȣ��Ǵ� �Լ�
        /// </summary>
        /// <param name="damageable"></param>
        protected abstract void OnThrough(Damageable damageable);
        /// <summary>
        /// ������ �������鿡 ���� ������ ��û�Ѵ�.
        /// </summary>
        protected void OnApplyDamage()
        {
            //print("������ ����");
            targetDamageableManager.ApplyDamage(GetInstanceID());
        }

        //protected abstract void OnShoot();
    }
}
