using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Controller;
using WOW.Projectile;

namespace WOW.Armament
{
    /// <summary>
    /// ���� ���� Ŭ����
    /// </summary>
    public class MainBattery : ArmamentBase
    {
        public Transform rootPosition;
        public Vector3 target;
        public GameObject HE;
        public GameObject AP;
        ShipController controller;
        public GameObject[] firePoint;

        GameObject bulletFactory;
        public int ShellID;

        [Range(0.0f, 1.0f)]
        public float rotSpeed = 0.05f;

        public Quaternion rotateOffset = Quaternion.Euler(0, 0.001f, 0);

        public float minX = 0;
        public float maxX = 30;
        
        public float minY = -90;
        public float maxY = 90;

        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponentInParent<ShipController>();
            if (HE != null)
            {
                bulletFactory = HE;
                ChangeBullet("HE");
            }
            else if (AP != null)
            {
                bulletFactory = AP;
                ChangeBullet("AP");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (controller != null)
            {
                target = controller.TargetPoint;            
            }
            else
            {
                target = transform.forward;
            }
            
            target.y = transform.position.y;

            Vector3 dir = (target - transform.position).normalized;
            Debug.DrawRay(transform.position, dir, Color.red);

            //float x = GetAngle((int)new Vector3(target.x, transform.position.y, target.z).magnitude);
            //x = Mathf.Clamp(x, minX, maxX);

            Quaternion rot = Quaternion.LookRotation(dir, transform.up) * rotateOffset;
            //rot.eulerAngles = new Vector3(x, Mathf.Clamp(Mathf.Repeat(rot.eulerAngles.y + 180, 360) - 180, minY, maxY), 0);
            rot.eulerAngles = new Vector3(0, Mathf.Clamp(Mathf.Repeat(rot.eulerAngles.y + 180, 360) - 180, minY, maxY), 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, rot, rotSpeed);
        }

        // �߻��� �� �ִ��� ���θ� ��ȯ�մϴ�.
        protected override bool CanFire()
        {
            // Ÿ���� ���ϴ� ���Ϳ� ���� ������ ���ϴ� ������ ������ ����
            // ������ �ʹ� Ŭ ��� ������ �ʴ´�.
            Vector3 dir = (target - transform.position).normalized;
            if (base.CanFire() && Mathf.Abs(Vector3.Dot(dir, transform.forward)) > 0.95f)
                return true;
            else
                return false;
        }

        // �߻��մϴ�.
        protected override void Fire()
        {
            if (firePoint == null)
                return;

            float angle = GetAngle((int)new Vector3(target.x, transform.position.y, target.z).magnitude);
            angle = Mathf.Clamp(angle, minX, maxX);
            
            // �ѱ��� ������ŭ �ݺ�
            for (int i = 0; i < firePoint.Length; i++)
            {
                // �ѱ� �տ� źȯ ����
                GameObject bullet = Instantiate(bulletFactory);
                bullet.transform.position = firePoint[i].transform.position;
                bullet.transform.rotation = firePoint[i].transform.rotation;
                bullet.GetComponent<Shell>().OnShoot(angle);
            }
        }

        /// <summary>
        /// źȯ�� �ٲ��ݴϴ�.
        /// </summary>
        /// <param name="bulletType">źȯ�� ���� ("AP", "HE")</param>
        public void ChangeBullet(string bulletType)
        {
            if (bulletType == "AP")
            {
                // źȯ�� AP�� �ٲ��ݴϴ�.
                bulletFactory = AP;
                // źȯ�� �̸��� ���� źȯ ID�� �����ɴϴ�.
                ShellID = BallisiticManager.Instance.GetShellID(HE.name);
            }
            else if (bulletType == "HE")
            {
                // źȯ�� HE�� �ٲ��ݴϴ�.
                bulletFactory = HE;
                // źȯ�� �̸��� ���� źȯ ID�� �����ɴϴ�.
                ShellID = BallisiticManager.Instance.GetShellID(HE.name);
            }
        }

        /// <summary>
        /// Ÿ�ٿ� ���߱� ���� ������ ��ȯ�մϴ�.
        /// </summary>
        /// <param name="targetX">Ÿ�ٰ��� X�� �Ÿ�</param>
        /// <returns></returns>
        float GetAngle(int targetX)
        {
            return BallisiticManager.Instance.GetAngle(ShellID, targetX * 100);
        }
    }
}
