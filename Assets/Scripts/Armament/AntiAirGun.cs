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

        // 사거리
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
                    // 발사 성공했으면 비행기에게 피격 이펙트 요청?
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
        
        // 발사할 수 있는지 여부를 반환합니다.
        protected override bool CanFire()
        {
            // 타겟을 향하는 벡터와 현재 정면을 향하는 벡터의 내적을 통해
            // 괴리가 너무 클 경우 발하지 않는다.
            Vector3 dir = (target - rootPosition.position).normalized;
            if (base.CanFire() && Vector3.Dot(dir, rootPosition.forward) > 0.95f)
                return true;
            else
                return false;
        }

        // 발사합니다.
        protected override void Fire()
        {
            if (firePoint == null || enemy == null)
                return;

            //float angle = GetAngle((int)(new Vector3(target.x, rootPosition.position.y, target.z) - rootPosition.position).magnitude);
            //float angle = GetAngle((int)(target - rootPosition.position).magnitude);
            //angle = Mathf.Clamp(angle, minX, maxX);
            float angle = 45;

            // 총구의 개수만큼 반복
            for (int i = 0; i < firePoint.Length; i++)
            {
                // 총구 앞에 탄환 생성
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
        /// 타겟에 맞추기 위한 각도를 반환합니다.
        /// </summary>
        /// <param name="targetX">타겟과의 X축 거리</param>
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
