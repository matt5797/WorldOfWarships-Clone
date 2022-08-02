using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Projectile
{
    public class PlayerFire : MonoBehaviour
    {
        // list -> 여러종류의 몬스터를 저장할 수 있어 
        // 배열

        public GameObject HE_Prefab; //프리팹
        public GameObject TD_Prefab; // 프리팹
        public GameObject realTD_Prefab; // 프리팹
        public Transform firepos;
        public Transform targetfirepos;

        [Header("HE 기능")]
        public float HE_speed = 2; // 미사일 속도
        public float HE_distanceFromStart = 6.0f; // 시작 지점을 기준으로 얼마나 꺾이는 지
        public float HE_distanceFromEnd = 3.0f; // 도착 지점을 기준으로 얼마나 꺾이는지
        public int HE_shotCount = 12; // 총 몇 개 발사
        [Range(0, 1)] public float HE_interval = 0.15f;
        public int HE_shotCountEveryInterval = 2; // 한번에 몇 개씩 발사

        [SerializeField]
        GameObject EnemyList;


        private void Update()
        {
            highexplosive();
            torpedo();
        }


        void highexplosive()
        {
            if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.Alpha1))
            {
                
                // 타겟 = 레이
                // 카메라 중점으로 
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                // 레이를 쐈을 때
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(ray, out hitInfo))
                {
                    // 이름이 추출된다
                    print(hitInfo.transform.name);
                    string name = hitInfo.transform.name;
                    // 그 이름이 enemylist에 있는 것이면
                    EnemyList = GameObject.Find(name);
                    // 쏜다
                    StartCoroutine(CreateHE());

                }
            }
           
            
        }

        void torpedo()
        {
            if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.Alpha3))
            {
                // 타겟 = 레이
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(ray, out hitInfo))
                {
                    // 이름이 추출된다
                    print(hitInfo.transform.name);
                    string name = hitInfo.transform.name;
                    // 그 이름이 enemylist에 있는 것이면
                    EnemyList = GameObject.Find(name);
                    // 공격
                    CreateTD();


                }
            }
        }

        IEnumerator CreateHE()
        {
            int _shotCount = HE_shotCount;
            while (_shotCount > 0)
            {
                for (int i = 0; i < HE_shotCountEveryInterval; i++)
                {
                    if (_shotCount > 0)
                    {
                        GameObject HighExplosive = Instantiate(HE_Prefab);
                        HighExplosive.GetComponent<HighExplosive>().Init(this.firepos.transform, EnemyList.transform, HE_speed, HE_distanceFromStart, HE_distanceFromEnd);

                        _shotCount--;
                    }
                }
                yield return new WaitForSeconds(HE_interval);
            }
            yield return null;
        }

        void CreateTD()
        {

            //타겟을 지정해야함, firpos의 forward 방향으로 던진 후 타겟을 공격
            // playerFire엔 생성하고, 타겟 지정만 하기
            //td를 생성하여 firepos 에서 출발
            Instantiate(TD_Prefab, firepos.transform.position, firepos.transform.rotation);

            
            Instantiate(realTD_Prefab, targetfirepos.transform.position, targetfirepos.transform.rotation);




        }
    }


}

