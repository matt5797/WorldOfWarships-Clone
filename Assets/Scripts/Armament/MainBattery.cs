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
            Vector3 targetDirection = target - rootPosition.position;

            float singleStep = Speed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(rootPosition.forward, targetDirection, singleStep, 0.0f);

            //Ÿ���� �ٶ󺸸� ȸ���ض�
            rootPosition.rotation = Quaternion.LookRotation(newDirection);

            rootPosition.eulerAngles = new Vector3(0, Mathf.Clamp(Mathf.Repeat(rootPosition.eulerAngles.y + 180, 360) - 180, -90, 90), 0);

        }

        // �߻��� �� �ִ��� ���θ� ��ȯ�մϴ�.
        protected override bool CanFire()
        {
            return true;
        }

        // �߻��մϴ�.
        protected override void Fire()
        {
            // �ѱ��� ������ŭ �ݺ�
            // �ѱ� �տ� źȯ ����
            // źȯ�� ��ũ��Ʈ�� �����Ͽ�, �ѱ��� ������ �����ְ�, �߻��ϵ��� ����Ѵ�.
            // źȯ.GetComponent<Ballistic>().OnShoot(���� �ѱ� ����)
        }
    }
}
