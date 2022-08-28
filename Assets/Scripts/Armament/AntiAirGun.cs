using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.BattleShip;
using WOW.Controller;
using WOW.Projectile;

namespace WOW.Armament
{
    public class AntiAirGun : ArmamentBase
    {
        public Shell bulletFactory;
        int ShellID;

        // ��Ÿ�
        public float attckRange = 100f;
        Aircraft enemy;

        LayerMask targetLayer;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            //StartCoroutine(AutoFire());
            ShellID = BallisiticManager.Instance.GetShellID(bulletFactory.shellName);
            if (rootPosition==null)
                rootPosition = transform;
        }

        void FixedUpdate()
        {
            enemy = GetClosestEnemy();

            if (enemy != null)
            {
                if (TryFire())
                {
                    // �߻� ���������� ����⿡�� �ǰ� ����Ʈ ��û?
                }
            }
        }

        Aircraft GetClosestEnemy()
        {
            RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position, attckRange, Vector3.up, 0, targetLayer);

            foreach (RaycastHit raycastHit in raycastHits)
            {
                Aircraft enemy = raycastHit.collider.GetComponent<Aircraft>();
                if (enemy == null)
                    continue;
                float distance = Vector3.Distance(rootPosition.position, enemy.transform.position);
                if (attckRange < distance)
                    continue;
                if (Vector3.Dot(rootPosition.parent.forward, (enemy.transform.position - rootPosition.position)) < 0)
                    continue;
                return enemy;
            }
            
            return null;
        }
        
        // �߻��� �� �ִ��� ���θ� ��ȯ�մϴ�.
        protected override bool CanFire()
        {
            // Ÿ���� ���ϴ� ���Ϳ� ���� ������ ���ϴ� ������ ������ ����
            // ������ �ʹ� Ŭ ��� ������ �ʴ´�.
            Vector3 dir = (target - rootPosition.position).normalized;
            if (base.CanFire() && Vector3.Dot(dir, rootPosition.forward) > 0.95f)
                return true;
            else
                return false;
        }

        // �߻��մϴ�.
        protected override void Fire()
        {
            if (firePoint == null || enemy == null)
                return;

            //float angle = GetAngle((int)(new Vector3(target.x, rootPosition.position.y, target.z) - rootPosition.position).magnitude);
            //float angle = GetAngle((int)(target - rootPosition.position).magnitude);
            //angle = Mathf.Clamp(angle, minX, maxX);
            float angle = 45;

            // �ѱ��� ������ŭ �ݺ�
            for (int i = 0; i < firePoint.Length; i++)
            {
                // �ѱ� �տ� źȯ ����
                Shell bullet = Instantiate<Shell>(bulletFactory);
                bullet.transform.position = firePoint[i].transform.position;
                bullet.transform.rotation = firePoint[i].transform.rotation;
                bullet.camp = controller.ship.camp;
                bullet.OnShoot(angle);
                if (muzzleFlashs.Length > i)
                    muzzleFlashs[i].Play();
            }

            //Managers.Sound.Play("secondary", Define.Sound.Effect);
        }

        /// <summary>
        /// Ÿ�ٿ� ���߱� ���� ������ ��ȯ�մϴ�.
        /// </summary>
        /// <param name="targetX">Ÿ�ٰ��� X�� �Ÿ�</param>
        /// <returns></returns>
        float GetAngle(int targetX)
        {
            return BallisiticManager.Instance.GetAngle(ShellID, targetX * 100);
        }


        IEnumerator AutoFire()
        {
            int count = 0;

            while(count++ < 3600)
            {
                TryFire();

                yield return new WaitForSeconds(2f);
            }
        }

        /*private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attckRange);
        }*/
    }
    
}
