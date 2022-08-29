using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Projectile;

namespace WOW.Armament
{
    public class TorpedoTube : ArmamentBase
    {
        public Torpedo bulletFactory;
        public int bulletCount = 4;
        public float fireAngle = 15;
        
        [Range(0.0f, 5f)]
        public float rotSpeed = 1f;
        public float minY = 0;

        public Vector3 TargetingPoint
        {
            get
            {
                return rootPosition.forward * 10;
            }
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

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
            
            float theta;
            // 총구의 개수만큼 반복
            for (int i = 0; i < bulletCount; i++)
            {
                // 총구 앞에 탄환 생성
                Torpedo bullet = Instantiate<Torpedo>(bulletFactory, firePoint[0].transform.position, firePoint[0].transform.rotation);
                bullet.camp = controller.ship.camp;
                theta = (i * fireAngle / (bulletCount - 1)) - (fireAngle / 2);
                bullet.transform.Rotate(0, theta, 0);
            }

             Managers.Sound.Play("torpedo", Define.Sound.Effect);
        }

    }
}

