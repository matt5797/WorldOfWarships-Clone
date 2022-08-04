using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Controller;

namespace WOW.Armament
{
    public class MainBattery : ArmamentBase
    {
        public Transform rootPosition;
        Vector3 ScreenCenter;
        public Vector3 target;
        public float Speed = 1;
        public GameObject bulletFactory;
        public Transform Firepos;
        float Angle;
        PlayerController PC;
        GameObject[] firePoint;

        // Start is called before the first frame update
        void Start()
        {
            PC = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            target = PC.TargetPoint;

            // 목적지 = 조준점
            // 타겟과 나의 거리
            Vector3 targetDirection = target - rootPosition.position;

            float singleStep = Speed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(rootPosition.forward, targetDirection, singleStep, 0.0f);

            //타겟을 바라보며 회전해라
            rootPosition.rotation = Quaternion.LookRotation(newDirection);

            rootPosition.eulerAngles = new Vector3(0, Mathf.Clamp(Mathf.Repeat(rootPosition.eulerAngles.y + 180, 360) - 180, -90, 90), 0);

        }

        // 발사할 수 있는지 여부를 반환합니다.
        protected override bool CanFire()
        {
            return true;
        }
        
        // 발사합니다.
        protected override void Fire()
        {
                print("1111111");
            // 총구의 개수만큼 반복
            for(int i = 0; i < firePoint.Length; i++)
            {
                // 총구 앞에 탄환 생성
                GameObject bullet = Instantiate(bulletFactory);
                bullet.transform.position = Firepos.transform.position;
                float angle = Vector3.Angle(firePoint[i].transform.position, transform.forward);
                // 탄환의 스크립트에 접근하여, 총구의 각도를 전해주고, 발사하도록 명령한다.
                // 탄환.GetComponent<Ballistic>().OnShoot(현재 총구 각도)
                bullet.GetComponent<Ballistic>().OnShoot(angle);
            }
        }
    }
}
