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

        // 발사할 수 있는지 여부를 반환합니다.
        protected override bool CanFire()
        {
            return true;
        }

        // 발사합니다.
        protected override void Fire()
        {
            //if (firePoint == null)
            //    return;

            int deltaAngle = 90 / bulletCount;
            // 총구의 개수만큼 반복
            for (int i = 0; i < bulletCount; i++)
            {
                // 총구 앞에 탄환 생성
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
           
