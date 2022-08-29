using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Controller;
using WOW.Projectile;

namespace WOW.Armament
{
    /// <summary>
    /// 배의 주포 클래스
    /// </summary>
    public class MainBattery : ArmamentBase
    {
        public Shell HE;
        public Shell AP;

        Shell bulletFactory;
        int ShellID;

        [Range(0.0f, 5f)]
        public float rotSpeed = 1f;

        public float minX = 0;
        public float maxX = 30;
        
        public float minY = 0;

        public Vector3 TargetingPoint
        {
            get
            {
                return rootPosition.position + rootPosition.forward * 10;
            }
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            if (HE != null)
            {
                bulletFactory = HE;
                ChangeBullet("HE");
            }
            else if (AP != null)
            {
                bulletFactory = AP;
                ChangeBullet("AP");
            }
        }
        
        // Update is called once per frame
        void FixedUpdate()
        {
            if (controller != null)
            {
                target = controller.TargetPoint;            
            }
            else
            {
                target = rootPosition.forward;
            }
            
            target.y = rootPosition.position.y;

            Vector3 dir = (target - rootPosition.position).normalized;

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
            if (firePoint == null)
                return;
            
            float angle = GetAngle((int)new Vector3(target.x, rootPosition.position.y, target.z).magnitude);
            angle = Mathf.Clamp(angle, minX, maxX);
            
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

            Managers.Sound.Play("mainbattery", Define.Sound.Effect);
        }

        /// <summary>
        /// 탄환을 바꿔줍니다.
        /// </summary>
        /// <param name="bulletType">탄환의 종류 ("AP", "HE")</param>
        public void ChangeBullet(string bulletType)
        {
            if (bulletType == "AP")
            {
                // 탄환을 AP로 바꿔줍니다.
                bulletFactory = AP;
                // 탄환의 이름을 통해 탄환 ID를 가져옵니다.
                ShellID = BallisiticManager.Instance.GetShellID(HE.shellName);
            }
            else if (bulletType == "HE")
            {
                // 탄환을 HE로 바꿔줍니다.
                bulletFactory = HE;
                // 탄환의 이름을 통해 탄환 ID를 가져옵니다.
                ShellID = BallisiticManager.Instance.GetShellID(HE.shellName);
            }
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(TargetingPoint, 0.5f);
        }
    }
}
