using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
   
    public float rotSpeed = 0;
    private RaycastHit hit;
    
    void Update()
    {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         Debug.DrawRay(ray.origin, ray.direction * 50.0f, Color.green);
         
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
         {
             Vector3 localPos = transform.InverseTransformPoint(hit.point);
             float angle = Mathf.Atan2(localPos.x, localPos.z) * Mathf.Rad2Deg;
             transform.Rotate(0, angle, 0);
         }

        


    }

}
