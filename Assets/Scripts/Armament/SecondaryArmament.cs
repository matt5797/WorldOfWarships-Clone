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

        // ��Ÿ�
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
            // �����׼� ���� ������ �߻� �Ҽ� �ִ� ���� ã�Ƴ���
            // �̰� �Ǵ��� �ٸ� ���� �����
            BattleShipBase[] enemyList = controller.admiral.enemy.battleShips;

            // �ڷ��� ���ʹ� 
            BattleShipBase nearestEnemy = null;
            float nearestDistance = float.MaxValue;
            
            foreach (BattleShipBase enemy in enemyList)
            {
                if (enemy.isDead)
                {
                    continue;
                }
                
                float distance = Vector3.Distance(rootPosition.position, enemy.transform.position);
                
                // ���� ������ ģ������ �Ǵ�
                if (attckRange < distance)
                    continue;

                if (Vector3.Dot(rootPosition.parent.forward, (enemy.transform.position - rootPosition.position)) < minY)
                    continue;
                
                // ������ ���� ����� �� ��������
                if (nearestDistance > distance)
                {
                    //nearest�� enemy�� �ִ´�
                    nearestEnemy = enemy;
                    nearestDistance = distance;
                }
                // ���ϰ� ����� ���� ���ϱ�
            }
            
            if (nearestEnemy != null)
            {
                return nearestEnemy;
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
            float angle = GetAngle((int)(target - rootPosition.position).magnitude);
            angle = Mathf.Clamp(angle, minX, maxX);

            if (GetInstanceID()== 518712)
            {
                Debug.LogWarning("angle : " + angle + "distance" + (int)(target - rootPosition.position).magnitude);
            }

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


            Managers.Sound.Play("secondary", Define.Sound.Effect);
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attckRange);
        }

    }
    

   
}
