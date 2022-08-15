using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Controller;

namespace WOW.Armament
{
    /// <summary>
    /// 배의 주포 클래스
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

            float x = GetAngle((int)new Vector3(target.x, transform.position.y, target.z).magnitude);
            x = Mathf.Clamp(x, minX, maxX);

            Quaternion rot = Quaternion.LookRotation(dir, transform.up);
            //rot.eulerAngles = new Vector3(x, Mathf.Clamp(Mathf.Repeat(rot.eulerAngles.y + 180, 360) - 180, minY, maxY), 0);
            rot.eulerAngles = new Vector3(x, Mathf.Clamp(Mathf.Repeat(rot.eulerAngles.y + 180, 360) - 180, minY, maxY), 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed);
        }

        // 발사할 수 있는지 여부를 반환합니다.
        /*protected override bool CanFire()
        {
            return true;
        }*/

        // 발사합니다.
        protected override void Fire()
        {
            if (firePoint == null)
                return;
            // 총구의 개수만큼 반복
            for (int i = 0; i < firePoint.Length; i++)
            {
                // 총구 앞에 탄환 생성
                GameObject bullet = Instantiate(bulletFactory);
                bullet.transform.position = firePoint[i].transform.position;
                bullet.transform.rotation = firePoint[i].transform.rotation;
            }
        }

        /// <summary>
        /// 탄환을 바꿔줍니다.
        /// </summary>
        /// <param name="bulletType">탄환의 종류 ("AP", "HE")</param>
        public void ChangeBullet(string bulletType)
        {
            if (bulletType == "AP")
            {
                // 탄환을 AP로 바꿔줍니다.
                bulletFactory = AP;
                // 탄환의 이름을 통해 탄환 ID를 가져옵니다.
                ShellID = BallisiticManager.Instance.GetShellID(HE.name);
            }
            else if (bulletType == "HE")
            {
                // 탄환을 HE로 바꿔줍니다.
                bulletFactory = HE;
                // 탄환의 이름을 통해 탄환 ID를 가져옵니다.
                ShellID = BallisiticManager.Instance.GetShellID(HE.name);
            }
        }

        /// <summary>
        /// 타겟에 맞추기 위한 각도를 반환합니다.
        /// </summary>
        /// <param name="targetX">타겟과의 X축 거리</param>
        /// <returns></returns>
        float GetAngle(int targetX)
        {
            return BallisiticManager.Instance.GetAngle(ShellID, targetX * 100);
        }
    }
}
