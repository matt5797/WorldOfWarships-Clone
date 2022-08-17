using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.BattleShip;
using WOW.Controller;

namespace WOW.Armament
{
    
    public class SecondaryArmament : ArmamentBase
    {

        /// <summary>
        /// 주포의 위치
        /// </summary>
        public enum GunSet
        {
            front,
            back
        }

        public GunSet gunSet = GunSet.front;    //현재 방향

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

        public GameObject you;


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


            StartCoroutine("Autofire");

            //transform.rotation = gunSet == GunSet.front ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(0, 180, 0);
            //root = gameObject.GetComponentInParent<BattleShip.BattleShipBase>().transform;
        }


        // Update is called once per frame
        void Update()
        { 
            target = Gettarget();

            Vector3 dir = (target - transform.position).normalized;
            Debug.DrawRay(transform.position, dir, Color.red);

            float x = GetAngle((int)new Vector3(target.x, transform.position.y, target.z).magnitude);
            x = Mathf.Clamp(x, 0, 30);

            Quaternion rot = Quaternion.LookRotation(dir, transform.up) * rotateOffset;
            rot.eulerAngles = new Vector3(x, Mathf.Clamp(Mathf.Repeat(rot.eulerAngles.y + 180, 360) - 180, minY, maxY), 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed);

        }
        
        float GetAngle(Vector2 start, Vector2 end)
        {
            Vector2 v2 = end - start;
            return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
            
        }

        public BattleShipBase[] enemyList;

        public float spherePadius;
        public float maxDistance;
        Vector3 direction;
        Vector3 Gettarget()
        {
            // 나한테서 가장 가깝고 발사 할수 있는 적을 찾아내기
            // 이걸 판단한 다른 집단 만들기
            
            // 사거리
            float range = 10f;
            // 자료형 에너미 
            BattleShipBase nearest = enemyList[0];
            foreach (var enemy in enemyList)
            {
                // 뭐가 적절한 친구인지 판단
                if(range > Vector3.Distance(transform.position, enemy.transform.position))
                {
                    continue;
                }
                Vector3 start = you.transform.position - transform.position;
                Vector3 end = transform.forward;
                float angle = GetAngle(new Vector2(start.x, start.z), new Vector2(end.x, end.z));
                // 각도에 따라서 
                if ((Mathf.Repeat(angle + 180, 360) - 180) < minY || (Mathf.Repeat(angle + 180, 360) - 180) > maxY)
                {
                    continue;
                }
                // 기존에 가장 가까운 애 가져오기
                if(Vector3.Distance(transform.position, nearest.transform.position) > Vector3.Distance(transform.position, enemy.transform.position))
                {
                   //nearest에 enemy를 넣는다
                    nearest = enemy;
                }
                // 나하고 상대의 각도 구하기
            }
                return nearest.transform.position;
                

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


        IEnumerator Autofire()
        {
            int count = 0;

            while(count++ < 100)
            {
                Fire();

                yield return new WaitForSeconds(2f);
            }
        }

    }
    

   
}
