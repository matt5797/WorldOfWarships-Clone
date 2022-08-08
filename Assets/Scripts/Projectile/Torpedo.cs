using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Data;

namespace WOW.Projectile
{
    public class Torpedo : ProjectileBase
    {
        public TorpedoData torpedoData;
  
       
        public float speed = 5;

        //좌표값 받아오는 변수
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

      
        protected override void OnImpact()
        {
            // 피격 시 이벤트
            // 어뢰의 y값을 저장한다
            torpedo_pos = (int)torpedo.transform.position.y;

            if (GameObject.Find("Monster"))
            {
                // 몬스터를 찾아 y값을 저장 한다
                monster_pos = (int)GameObject.Find("Monster").transform.position.y;

                // 만약 어뢰와 몬스터의 y 값이 같다면
                if (torpedo == monster)
                {
                    // 어뢰가 몬스터와의 y값이 같은 곳에 리지드 바디의 y 값을 고정하고
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                    // 어뢰가 몬스터를 향해 달려 나간다
                    transform.position = Vector3.MoveTowards(gameObject.transform.position, monster.transform.position, speed * Time.deltaTime);
                }

            }
            else
            {
                // 만약 몬스터가 없을 시 그냥 바닥에 떨어진다
            }
        }
        
      
    }
}