using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.BattleShip;
using WOW.Controller;
using WOW.Projectile;

namespace WOW.Armament
{
    
    public class SecondaryArmament : ArmamentBase
    {
        public Shell bulletFactory;
        int ShellID;

        [Range(0.0f, 5f)]
        public float rotSpeed = 1f;

        public float minX = 0;
        public float maxX = 30;

        public float minY = 0;

        // 사거리
        public float attckRange = 100f;
        BattleShipBase enemy;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            StartCoroutine(AutoFire());
            ShellID = BallisiticManager.Instance.GetShellID(bulletFactory.shellName);
        }

        void FixedUpdate()
        {
            enemy = GetClosestEnemy();
            
            if (enemy!=null)
            {
                target = enemy.transform.position;
                target.y = rootPosition.position.y;

                Vector3 dir = (target - rootPosition.position).normalized;
                Debug.DrawRay(rootPosition.position, dir, Color.red);
                Debug.DrawRay(rootPosition.position, rootPosition.parent.forward, Color.green);

                float singleStep = rotSpeed * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(rootPosition.forward, dir, singleStep, 0.0f);
                Quaternion rot = Quaternion.LookRotation(newDirection);
                Vector3 temp = rootPosition.eulerAngles;
                rootPosition.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);
                if (Vector3.Dot(rootPosition.parent.forward, rootPosition.forward) < minY)
                {
                    rootPosition.eulerAngles = temp;
                }
            }
        }

        /*float GetAngle(Vector2 start, Vector2 end)
        {
            Vector2 v2 = end - start;
            return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        }*/

        BattleShipBase GetClosestEnemy()
        {
            // 나한테서 가장 가깝고 발사 할수 있는 적을 찾아내기
            // 이걸 판단한 다른 집단 만들기
            BattleShipBase[] enemyList = controller.admiral.enemy.battleShips;

            // 자료형 에너미 
            BattleShipBase nearestEnemy = null;
            float nearestDistance = float.MaxValue;
            
            foreach (BattleShipBase enemy in enemyList)
            {
                if (enemy.isDead)
                {
                    continue;
                }
                
                float distance = Vector3.Distance(rootPosition.position, enemy.transform.position);
                
                // 뭐가 적절한 친구인지 판단
                if (attckRange < distance)
                    continue;

                if (Vector3.Dot(rootPosition.parent.forward, (enemy.transform.position - rootPosition.position)) < minY)
                    continue;
                
                // 기존에 가장 가까운 애 가져오기
                if (nearestDistance > distance)
                {
                    //nearest에 enemy를 넣는다
                    nearestEnemy = enemy;
                    nearestDistance = distance;
                }
                // 나하고 상대의 각도 구하기
            }
            
            if (nearestEnemy != null)
            {
                return nearestEnemy;
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
            float angle = GetAngle((int)(target - rootPosition.position).magnitude);
            angle = Mathf.Clamp(angle, minX, maxX);

            if (GetInstanceID()== 518712)
            {
                Debug.LogWarning("angle : " + angle + "distance" + (int)(target - rootPosition.position).magnitude);
            }

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

            Managers.Sound.Play("secondary", Define.Sound.Effect);
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attckRange);
        }

    }
    

   
}
