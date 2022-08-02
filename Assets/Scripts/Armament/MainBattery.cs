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

        // �߻��� �� �ִ��� ���θ� ��ȯ�մϴ�.
        protected override bool CanFire()
        {
            return true;
        }

        // �߻��մϴ�.
        protected override void Fire()
        {
            // ������ = ������
            // Ÿ�ٰ� ���� �Ÿ�
            Vector3 targetDirection = target.position - transform.position;

            float singleStep = Speed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            //Ÿ���� �ٶ󺸸� ȸ���ض�
            transform.rotation = Quaternion.LookRotation(newDirection);

            Angle = Mathf.Repeat(Angle + 180, 360) - 180;

            transform.eulerAngles = new Vector3(0, Mathf.Clamp(Mathf.Repeat(transform.eulerAngles.y + 180, 360) - 180, -90, 90), 0);

            print(transform.eulerAngles);


        }


    }
}
