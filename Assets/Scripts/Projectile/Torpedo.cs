using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.DamageSystem;
using WOW.Data;

namespace WOW.Projectile
{
    public class Torpedo : ProjectileBase
    {
        public TorpedoData torpedoData;
  
       
        public float speed = 5;

        //��ǥ�� �޾ƿ��� ����
        public int torpedo_pos;
        public int monster_pos;

        public Transform torpedo;
        public Transform monster;
        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }

      
        protected override void OnImpact(Damageable damageable)
        {
            // �ǰ� �� �̺�Ʈ
            // ����� y���� �����Ѵ�
            torpedo_pos = (int)torpedo.transform.position.y;

            if (GameObject.Find("Monster"))
            {
                // ���͸� ã�� y���� ���� �Ѵ�
                monster_pos = (int)GameObject.Find("Monster").transform.position.y;

                // ���� ��ڿ� ������ y ���� ���ٸ�
                if (torpedo == monster)
                {
                    // ��ڰ� ���Ϳ��� y���� ���� ���� ������ �ٵ��� y ���� �����ϰ�
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                    // ��ڰ� ���͸� ���� �޷� ������
                    transform.position = Vector3.MoveTowards(gameObject.transform.position, monster.transform.position, speed * Time.deltaTime);
                }

            }
            else
            {
                // ���� ���Ͱ� ���� �� �׳� �ٴڿ� ��������
            }
        }

        protected override void OnThrough(Damageable damageable)
        {
            throw new NotImplementedException();
        }
    }
}