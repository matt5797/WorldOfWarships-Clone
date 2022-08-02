using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Armament
{
    public class MainBattery : ArmamentBase
    {
        Vector3 ScreenCenter;
        public Transform target;
        public float Speed = 1;
        float Angle;

        // Start is called before the first frame update
        void Start()
        {

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
            // 목적지 = 조준점
            // 타겟과 나의 거리
            Vector3 targetDirection = target.position - transform.position;

            float singleStep = Speed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            //타겟을 바라보며 회전해라
            transform.rotation = Quaternion.LookRotation(newDirection);

            Angle = Mathf.Repeat(Angle + 180, 360) - 180;

            transform.eulerAngles = new Vector3(0, Mathf.Clamp(Mathf.Repeat(transform.eulerAngles.y + 180, 360) - 180, -90, 90), 0);

            print(transform.eulerAngles);


        }


    }
}
