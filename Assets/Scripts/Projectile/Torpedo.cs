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
        // 해수면에 향해 떨어지고 싶다
        // 해수면에 닿으면 앞으로 날아가고 싶다
        public float speed = 5;
        public float range = 60;

        Vector3 startPosition;

        public int damage;
        public GameObject[] explosionParticles; // 폭발시 플레이할 파티클들
        public GameObject[] trailParticles;

        private void Start()
        {
            startPosition = transform.position;
        }

        void Update()
        {
            // 만약 어뢰와 물의 y 값이 같다면
            if (transform.position.y <= 0)
            {
                // 어뢰가 물의 y값이 같은 곳에 리지드 바디의 y 값을 고정하고
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                // 어뢰가 앞을 향해 달려 나간다
                transform.Translate(gameObject.transform.forward * speed * Time.deltaTime, Space.World);

                foreach (GameObject trail in trailParticles)
                {
                    trail.SetActive(true);
                    trail.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
                }
            }

            if ((transform.position - startPosition).magnitude > range)
            {
                Destroy(gameObject);
            }
        }

        protected override void OnThrough(Damageable damageable)
        {
            throw new NotImplementedException();
        }

        protected override void OnImpact(Damageable damageable)
        {
            print("Torpedo Impact");
            damageable.CheckDamage(damage, GetInstanceID());
            OnExplosion();
        }

        /// <summary>
        /// 탄환 폭발 시 호출되는 함수
        /// </summary>
        void OnExplosion()
        {

            // 추후 확산 판정 삽입?
            //print("OnExplosion");
            foreach (GameObject particleFactory in explosionParticles)
            {
                GameObject particle = Instantiate(particleFactory);
                particle.transform.position = transform.position;
                //particle.Play();
            }
            //Managers.Sound.Play("Bomb", Define.Sound.Effect);
            OnApplyDamage();
            Destroy(gameObject);
        }
    }
}