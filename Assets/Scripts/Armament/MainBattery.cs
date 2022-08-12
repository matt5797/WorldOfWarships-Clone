using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Controller;

namespace WOW.Armament
{
    /// <summary>
    /// ���� ���� Ŭ����
    /// </summary>
    public class MainBattery : ArmamentBase
    {
        /// <summary>
        /// ������ ��ġ
        /// </summary>
        public enum GunSet
        {
            front,
            back
        }
        
        public GunSet gunSet = GunSet.front;    //���� ����
        
        //public Transform rootPosition;
        //Vector3 ScreenCenter;
        public Vector3 target;
        //public float Speed = 1;
        public GameObject HE;
        public GameObject AP;
        ShipController controller;
        public GameObject[] firePoint;

        GameObject bulletFactory;
        public int ShellID;

        //public Transform root;
        //public Vector3 target;
        [Range(0.0f, 1.0f)]
        public float rotSpeed = 0.1f;

        public Quaternion rotateOffset = Quaternion.Euler(0, 0.001f, 0);
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
            //transform.rotation = gunSet == GunSet.front ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(0, 180, 0);
            //root = gameObject.GetComponentInParent<BattleShip.BattleShipBase>().transform;
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

/*
            Vector3 dir = (target - transform.position).normalized;
            Debug.DrawRay(transform.position, dir, Color.red);

            float x = GetAngle((int)new Vector3(target.x, transform.position.y, target.z).magnitude);
            x = Mathf.Clamp(x, 0, 30);

            Quaternion rot = Quaternion.LookRotation(dir, transform.up) * rotateOffset;
            rot.eulerAngles = new Vector3(x, Mathf.Clamp(Mathf.Repeat(rot.eulerAngles.y + 180, 360) - 180, minY, maxY), 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed);
*/
            
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
