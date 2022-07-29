using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
     public GameObject player; // 바라볼 플레이어 오브젝트
     public float xmove = 0; 
     public float ymove = 0;  
     public float distance = 0;



     private void Update()
     {
        xmove += Input.GetAxis("Mouse X");
        ymove -= Input.GetAxis("Mouse Y"); 
         transform.rotation = Quaternion.Euler(ymove, xmove, 0); 

         Vector3 reverseDistance = new Vector3(0.0f, 0.0f, distance); 

         transform.position = player.transform.position - transform.rotation * reverseDistance; 

     }

    }






