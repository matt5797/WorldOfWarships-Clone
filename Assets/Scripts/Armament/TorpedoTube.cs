using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Armament
{
    public class TorpedoTube : ArmamentBase
    {
        // public GameObject[] firePoint;
        public GameObject bulletFactory;
        public int bulletCount = 3;

        // Start is called before the first frame update
        void Start()
        {
            Invoke("Fire", 2f);
           
        }

        // Update is called once per frame
        void Update()
        {

        }

        // �߻��� �� �ִ��� ���θ� ��ȯ�մϴ�.
        protected override bool CanFire()
        {
            return true;
        }

        // �߻��մϴ�.
        protected override void Fire()
        {
            //if (firePoint == null)
            //    return;

            int deltaAngle = 90 / bulletCount;
            // �ѱ��� ������ŭ �ݺ�
            for (int i = 0; i < bulletCount; i++)
            {
                // �ѱ� �տ� źȯ ����
                GameObject bullet = Instantiate(bulletFactory);
                float theta = i * deltaAngle;
                float r = 1;
                float x = r * Mathf.Cos(Mathf.Deg2Rad * theta);
                float y = r * Mathf.Sin(Mathf.Deg2Rad * theta);

                bullet.transform.eulerAngles = new Vector3(0, deltaAngle * i, 0);
                bullet.transform.position = transform.position;
                

               //  bullet.transform.position = firePoint[i].transform.position;
               //  bullet.transform.rotation = firePoint[i].transform.rotation;

            }
        }

    }
}
           
