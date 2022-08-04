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
        float Angle;
        PlayerController PC;
        public GameObject[] firePoint;

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
            if (firePoint == null)
                return;
            // �ѱ��� ������ŭ �ݺ�
            for (int i = 0; i < firePoint.Length; i++)
            {
                // �ѱ� �տ� źȯ ����
                GameObject bullet = Instantiate(bulletFactory);
                bullet.transform.position = firePoint[i].transform.position;
                bullet.transform.rotation = firePoint[i].transform.rotation;
                float angle = Vector3.SignedAngle(Vector3.up, transform.up, transform.right) * -1;
                // źȯ�� ��ũ��Ʈ�� �����Ͽ�, �ѱ��� ������ �����ְ�, �߻��ϵ��� ����Ѵ�.
                // źȯ.GetComponent<Ballistic>().OnShoot(���� �ѱ� ����)
                bullet.GetComponent<Ballistic>().OnShoot(angle);
            }
        }
    }
}
