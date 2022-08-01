using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Projectile
{
    public class PlayerFire : MonoBehaviour
    {
        // list -> ���������� ���͸� ������ �� �־� 
        // �迭

        public GameObject HE_Prefab; //������
        public GameObject TD_Prefab; // ������
        public GameObject realTD_Prefab; // ������
        public Transform firepos;
        public Transform targetfirepos;

        [Header("HE ���")]
        public float HE_speed = 2; // �̻��� �ӵ�
        public float HE_distanceFromStart = 6.0f; // ���� ������ �������� �󸶳� ���̴� ��
        public float HE_distanceFromEnd = 3.0f; // ���� ������ �������� �󸶳� ���̴���
        public int HE_shotCount = 12; // �� �� �� �߻�
        [Range(0, 1)] public float HE_interval = 0.15f;
        public int HE_shotCountEveryInterval = 2; // �ѹ��� �� ���� �߻�

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
                
                // Ÿ�� = ����
                // ī�޶� �������� 
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                // ���̸� ���� ��
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(ray, out hitInfo))
                {
                    // �̸��� ����ȴ�
                    print(hitInfo.transform.name);
                    string name = hitInfo.transform.name;
                    // �� �̸��� enemylist�� �ִ� ���̸�
                    EnemyList = GameObject.Find(name);
                    // ���
                    StartCoroutine(CreateHE());

                }
            }
           
            
        }

        void torpedo()
        {
            if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.Alpha3))
            {
                // Ÿ�� = ����
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(ray, out hitInfo))
                {
                    // �̸��� ����ȴ�
                    print(hitInfo.transform.name);
                    string name = hitInfo.transform.name;
                    // �� �̸��� enemylist�� �ִ� ���̸�
                    EnemyList = GameObject.Find(name);
                    // ����
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

            //Ÿ���� �����ؾ���, firpos�� forward �������� ���� �� Ÿ���� ����
            // playerFire�� �����ϰ�, Ÿ�� ������ �ϱ�
            //td�� �����Ͽ� firepos ���� ���
            Instantiate(TD_Prefab, firepos.transform.position, firepos.transform.rotation);

            
            Instantiate(realTD_Prefab, targetfirepos.transform.position, targetfirepos.transform.rotation);




        }
    }


}

