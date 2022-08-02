using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Controller;

namespace WOW.Armament
{
    public class MainBattery : ArmamentBase
    {
        Vector3 ScreenCenter;
        public Vector3 target;
        public float Speed = 1;
        float Angle;
        PlayerController PC;

        // Start is called before the first frame update
        void Start()
        {
            PC = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            target = PC.TargetPoint;

            // ������ = ������
            // Ÿ�ٰ� ���� �Ÿ�
            Vector3 targetDirection = target - transform.position;

            float singleStep = Speed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            //Ÿ���� �ٶ󺸸� ȸ���ض�
            transform.rotation = Quaternion.LookRotation(newDirection);

            Angle = Mathf.Repeat(Angle + 180, 360) - 180;

            transform.eulerAngles = new Vector3(0, Mathf.Clamp(Mathf.Repeat(transform.eulerAngles.y + 180, 360) - 180, -90, 90), 0);

            print(transform.eulerAngles);

        }

        // �߻��� �� �ִ��� ���θ� ��ȯ�մϴ�.
        protected override bool CanFire()
        {
            return true;
        }

        // �߻��մϴ�.
        protected override void Fire()
        {
            

        }


    }
}
