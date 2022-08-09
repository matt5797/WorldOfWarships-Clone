using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Controller;

namespace WOW.Armament
{

    public class MainBattery : ArmamentBase
    {
        public enum GunSet
        {
            front,
            back
        }

        public GunSet gunSet = GunSet.front;
        
        public Transform rootPosition;
        Vector3 ScreenCenter;
        public Vector3 target;
        public float Speed = 1;
        public GameObject HE;
        public GameObject AP;
        PlayerController PC;
        public GameObject[] firePoint;

        GameObject bulletFactory;
        public int ShellID;

        public Transform root;
        //public Vector3 target;
        [Range(0.0f, 1.0f)]
        public float rotSpeed = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
            PC = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
            bulletFactory = HE;
            ChangeBullet("HE");
            //transform.rotation = gunSet == GunSet.front ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(0, 180, 0);
            root = gameObject.GetComponentInParent<BattleShip.BattleShipBase>().transform;
        }

        // Update is called once per frame
        void Update()
        {
            target = PC.TargetPoint;

            // ������ = ������
            // Ÿ�ٰ� ���� �Ÿ�
            //Vector3 targetDirection = target - transform.position;
            //print(target + " / " + firePoint[0].transform.position + " / " + targetDirection);

            //float singleStep = Speed * Time.deltaTime;

            //Vector3 newDirection = Vector3.RotateTowards(firePoint[0].transform.forward, targetDirection, singleStep, 0.0f);
            //Vector3 newDirection = Vector3.RotateTowards(firePoint[0].transform.forward * 10, targetDirection, singleStep, 0.0f);
            //print(newDirection);
            //Ÿ���� �ٶ󺸸� ȸ���ض�
            //transform.rotation = Quaternion.LookRotation(newDirection);
            //transform.rotation = Quaternion.LookRotation(newDirection);
            //float x = GetAngle((int)new Vector3(target.x, transform.position.y, target.z).magnitude) * -1;
            //x = Mathf.Clamp(x, -45, 0);
            //transform.localEulerAngles = new Vector3(0, Mathf.Clamp(Mathf.Repeat(transform.localEulerAngles.y + 180, 360) - 180, -90, 90), 0);

            
            target.y = transform.position.y;

            Vector3 dir = (target - transform.position).normalized;

            float frontBack = Vector3.Dot(root.forward, dir) ;

            float x = GetAngle((int)new Vector3(target.x, transform.position.y, target.z).magnitude);
            x = Mathf.Clamp(x, 0, 30);
            
            if (gunSet == GunSet.front && frontBack >= -0.6)
            {
                //transform.rotation = Quaternion.LookRotation(dir);
                Quaternion rot = Quaternion.LookRotation(dir, transform.up);
                rot.eulerAngles = new Vector3(-x, rot.eulerAngles.y, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed);
            }
            else if (gunSet == GunSet.back && frontBack <= 0.6)
            {
                //transform.rotation = Quaternion.LookRotation(dir);
                Quaternion rot = Quaternion.LookRotation(dir * -1, transform.up);
                rot.eulerAngles = new Vector3(x, rot.eulerAngles.y, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed);
            }
        }

        // �߻��� �� �ִ��� ���θ� ��ȯ�մϴ�.
        /*protected override bool CanFire()
        {
            return true;
        }*/

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
                //float angle = Vector3.SignedAngle(Vector3.up, transform.up, transform.right) * -1;
                // źȯ�� ��ũ��Ʈ�� �����Ͽ�, �ѱ��� ������ �����ְ�, �߻��ϵ��� ����Ѵ�.
                // źȯ.GetComponent<Ballistic>().OnShoot(���� �ѱ� ����)
                //bullet.GetComponent<Ballistic>().OnShoot(angle);
            }
        }

        public void ChangeBullet(string bulletType)
        {
            if (bulletType == "AP")
            {
                bulletFactory = AP;
                ShellID = BallisiticManager.Instance.GetShellID(HE.name);
                print(ShellID);
            }
            else if (bulletType == "HE")
            {
                bulletFactory = HE;
                ShellID = BallisiticManager.Instance.GetShellID(HE.name);
                print(ShellID);
            }
        }

        float GetAngle(int targetX)
        {
            return BallisiticManager.Instance.GetAngle(ShellID, targetX * 100);
        }
    }
    
    /*
    public class MainBattery : ArmamentBase
    {
        public Transform rootPosition;
        Vector3 ScreenCenter;
        public Vector3 target;
        public float Speed = 1;
        public GameObject HE;
        public GameObject AP;
        PlayerController PC;
        public GameObject[] firePoint;

        GameObject bulletFactory;
        public int ShellID;

        // Start is called before the first frame update
        void Start()
        {
            PC = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
            bulletFactory = HE;
            ChangeBullet("HE");
        }

        // Update is called once per frame
        void Update()
        {
            target = PC.TargetPoint;

            // ������ = ������
            // Ÿ�ٰ� ���� �Ÿ�
            Vector3 targetDirection = target - firePoint[0].transform.position;
            print(target + " / " + firePoint[0].transform.position + " / " + targetDirection);

            float singleStep = Speed * Time.deltaTime;

            //Vector3 newDirection = Vector3.RotateTowards(firePoint[0].transform.forward, targetDirection, singleStep, 0.0f);
            Vector3 newDirection = Vector3.RotateTowards(firePoint[0].transform.forward*10, targetDirection, singleStep, 0.0f);
            //print(newDirection);
            //Ÿ���� �ٶ󺸸� ȸ���ض�
            transform.rotation = Quaternion.LookRotation(newDirection);
            //transform.rotation = Quaternion.LookRotation(newDirection);
            float x = GetAngle((int)new Vector3(target.x, transform.position.y, target.z).magnitude) * -1;
            x = Mathf.Clamp(x, -45, 0);
            //transform.localEulerAngles = new Vector3(x, Mathf.Clamp(Mathf.Repeat(transform.localEulerAngles.y + 180, 360) - 180, -90, 90), 0);
        }

        // �߻��� �� �ִ��� ���θ� ��ȯ�մϴ�.
        *//*protected override bool CanFire()
        {
            return true;
        }*//*
        
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
                //float angle = Vector3.SignedAngle(Vector3.up, transform.up, transform.right) * -1;
                // źȯ�� ��ũ��Ʈ�� �����Ͽ�, �ѱ��� ������ �����ְ�, �߻��ϵ��� ����Ѵ�.
                // źȯ.GetComponent<Ballistic>().OnShoot(���� �ѱ� ����)
                //bullet.GetComponent<Ballistic>().OnShoot(angle);
            }
        }

        public void ChangeBullet(string bulletType)
        {
            if (bulletType == "AP")
            {
                bulletFactory = AP;
                ShellID = BallisiticManager.Instance.GetShellID(HE.name);
            }
            else if (bulletType == "HE")
            {
                bulletFactory = HE;
                ShellID = BallisiticManager.Instance.GetShellID(HE.name);
            }
        }

        float GetAngle(int targetX)
        {
            return BallisiticManager.Instance.GetAngle(ShellID, targetX * 100);
        }
    }*/
}
