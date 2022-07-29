using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 3f;
    public float rotateSpeed = 1f;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

  
    void Move()
    {

       float h = Input.GetAxis("Horizontal");
       float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;

        dir.Normalize();
    
       if (!(h == 0 && v == 0))
        {
            //플레이어
            transform.position += dir * speed * Time.deltaTime;

            //플레이어 회전
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
        }
    }


}
