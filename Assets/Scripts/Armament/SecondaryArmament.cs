using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Controller;

namespace WOW.Armament
{

    public class SecondaryArmament : ArmamentBase
    {
        public enum GunSet
        {
            front,
            back
        }

        public GunSet gunSet = GunSet.front;
        
        Vector3 target;
        public GameObject HE;
        PlayerController PC;
        public GameObject[] firePoint;

        GameObject bulletFactory;
        int ShellID;

        Transform root;
        [Range(0.0f, 1.0f)]
        public float rotSpeed = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
            PC = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
            bulletFactory = HE;
            ChangeBullet("HE");
            root = gameObject.GetComponentInParent<BattleShip.BattleShipBase>().transform;
        }

        // Update is called once per frame
        void Update()
        {
            target = PC.TargetPoint;
            
            target.y = transform.position.y;

            Vector3 dir = (target - transform.position).normalized;

            float frontBack = Vector3.Dot(root.forward, dir) ;

            float x = GetAngle((int)new Vector3(target.x, transform.position.y, target.z).magnitude);
            x = Mathf.Clamp(x, 0, 30);
            
            if (gunSet == GunSet.front && frontBack >= -0.6)
            {
                Quaternion rot = Quaternion.LookRotation(dir, transform.up);
                rot.eulerAngles = new Vector3(-x, rot.eulerAngles.y, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed);
            }
            else if (gunSet == GunSet.back && frontBack <= 0.6)
            {
                Quaternion rot = Quaternion.LookRotation(dir * -1, transform.up);
                rot.eulerAngles = new Vector3(x, rot.eulerAngles.y, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed);
            }
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

        public void ChangeBullet(string bulletType)
        {
            if (bulletType == "HE")
            {
                bulletFactory = HE;
                ShellID = BallisiticManager.Instance.GetShellID(HE.name);
            }
        }

        float GetAngle(int targetX)
        {
            return BallisiticManager.Instance.GetAngle(ShellID, targetX * 100);
        }
    }
}
