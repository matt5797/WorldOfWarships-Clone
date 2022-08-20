using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Armament
{
    public class TorpedoTube : ArmamentBase
    {
        public GameObject bulletFactory;
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
            if (firePoint == null)
                return;
            
            float theta;
            // �ѱ��� ������ŭ �ݺ�
            for (int i = 0; i < bulletCount; i++)
            {
                // �ѱ� �տ� źȯ ����
                GameObject bullet = Instantiate(bulletFactory, firePoint[0].transform.position, firePoint[0].transform.rotation);
                theta = (i * fireAngle / (bulletCount - 1)) - (fireAngle / 2);
                bullet.transform.Rotate(0, theta, 0);
            }
        }

    }
}

