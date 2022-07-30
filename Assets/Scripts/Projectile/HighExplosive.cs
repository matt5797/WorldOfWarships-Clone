using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Projectile
{

    public class HighExplosive : MonoBehaviour
    {
        public float force = 2000.0f;
        //public GameObject expEffect;
        

        private void Start()
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force);
            Destroy(this.gameObject, 10.0f);
        }

        private void Update()
        {
           
        }

        // public GameObject HE_Effect;
      //  private void OnCollisionEnter(Collision collision)
      //  {
              //GameObject effect = Instantiate(HE_Effect);
              //effect.transform.position = transform.position;

              // GameObject obj = Instantiate(expEffect, transform.forward, Quaternion.identity);
              // Destroy(this.gameObject);
              // Destroy(gameObject, 2.0f);
      //   }
    }
}