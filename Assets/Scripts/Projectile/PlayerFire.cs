using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform FirePos;
    
    public GameObject Bullet;
    // ÃÑ¾Ë °¹¼ö
    public int bulletCount = 0;
 
   // public float throwPower = 0;


    private void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //180µµ ¹üÀ§ ¾È¿¡
            int deltAngle = 180 / bulletCount;
            for(int i = 0; i < bulletCount; i++)
            {
             
                float theta = i * deltAngle;
                float r = 1;
                float x = r * Mathf.Cos(Mathf.Deg2Rad * theta);
                float y = r * Mathf.Sin(Mathf.Deg2Rad * theta);
                Bullet.transform.eulerAngles = new Vector3(0, 0, deltAngle * i);
              
                Instantiate(Bullet, FirePos.position, FirePos.rotation);

                //  GameObject bullet = Instantiate(Bullet);

                //  bullet.transform.position = FirePos.transform.forward;

                //  Rigidbody rb = GetComponent<Rigidbody>();

                //  rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);

            }

        }
    }
}
