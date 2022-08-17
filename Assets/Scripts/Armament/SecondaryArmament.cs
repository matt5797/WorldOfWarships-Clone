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
            // �����׼� ���� ������ �߻� �Ҽ� �ִ� ���� ã�Ƴ���
            // �̰� �Ǵ��� �ٸ� ���� �����
            
            // ��Ÿ�
            float range = 10f;
            // �ڷ��� ���ʹ� 
            BattleShipBase nearest = enemyList[0];
            foreach (var enemy in enemyList)
            {
                // ���� ������ ģ������ �Ǵ�
                if(range > Vector3.Distance(transform.position, enemy.transform.position))
                {
                    continue;
                }
                Vector3 start = you.transform.position - transform.position;
                Vector3 end = transform.forward;
                float angle = GetAngle(new Vector2(start.x, start.z), new Vector2(end.x, end.z));
                // ������ ���� 
                if ((Mathf.Repeat(angle + 180, 360) - 180) < minY || (Mathf.Repeat(angle + 180, 360) - 180) > maxY)
                {
                    continue;
                }
                // ������ ���� ����� �� ��������
                if(Vector3.Distance(transform.position, nearest.transform.position) > Vector3.Distance(transform.position, enemy.transform.position))
                {
                   //nearest�� enemy�� �ִ´�
                    nearest = enemy;
                }
                // ���ϰ� ����� ���� ���ϱ�
            }
                return nearest.transform.position;
                

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
