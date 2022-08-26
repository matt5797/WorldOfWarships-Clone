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
        public Shell HE;
        public Shell AP;

        Shell bulletFactory;
        int ShellID;

        [Range(0.0f, 5f)]
        public float rotSpeed = 1f;

        public float minX = 0;
        public float maxX = 30;
        
        public float minY = 0;

        public Vector3 TargetingPoint
        {
            get
            {
                return rootPosition.position + rootPosition.forward * 10;
            }
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
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
        void FixedUpdate()
        {
            if (controller != null)
            {
                target = controller.TargetPoint;            
            }
            else
            {
                target = rootPosition.forward;
            }
            
            target.y = rootPosition.position.y;

            Vector3 dir = (target - rootPosition.position).normalized;

            float singleStep = rotSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(rootPosition.forward, dir, singleStep, 0.0f);
            Quaternion rot = Quaternion.LookRotation(newDirection);
            Vector3 temp = rootPosition.eulerAngles;
            rootPosition.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);
            if (Vector3.Dot(rootPosition.parent.forward, rootPosition.forward) < minY)
            {
                rootPosition.eulerAngles = temp;
            }
        }

        // �߻��� �� �ִ��� ���θ� ��ȯ�մϴ�.
        protected override bool CanFire()
        {
            // Ÿ���� ���ϴ� ���Ϳ� ���� ������ ���ϴ� ������ ������ ����
            // ������ �ʹ� Ŭ ��� ������ �ʴ´�.
            Vector3 dir = (target - rootPosition.position).normalized;
            if (base.CanFire() && Vector3.Dot(dir, rootPosition.forward) > 0.95f)
                return true;
            else
                return false;
        }

        // �߻��մϴ�.
        protected override void Fire()
        {
            if (firePoint == null)
                return;

            float angle = GetAngle((int)new Vector3(target.x, rootPosition.position.y, target.z).magnitude);
            angle = Mathf.Clamp(angle, minX, maxX);
            
            // �ѱ��� ������ŭ �ݺ�
            for (int i = 0; i < firePoint.Length; i++)
            {
                // �ѱ� �տ� źȯ ����
                Shell bullet = Instantiate<Shell>(bulletFactory);
                bullet.transform.position = firePoint[i].transform.position;
                bullet.transform.rotation = firePoint[i].transform.rotation;
                bullet.camp = controller.ship.camp;
                bullet.OnShoot(angle);
            }

            Managers.Sound.Play("mainbattery", Define.Sound.Effect);
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
                ShellID = BallisiticManager.Instance.GetShellID(HE.shellName);
            }
            else if (bulletType == "HE")
            {
                // źȯ�� HE�� �ٲ��ݴϴ�.
                bulletFactory = HE;
                // źȯ�� �̸��� ���� źȯ ID�� �����ɴϴ�.
                ShellID = BallisiticManager.Instance.GetShellID(HE.shellName);
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(TargetingPoint, 0.5f);
        }
    }
}
