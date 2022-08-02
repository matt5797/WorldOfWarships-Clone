using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl1 : MonoBehaviour
{
    void Update()
    {
         
            
        
        transform.eulerAngles = new Vector3(0, Mathf.Clamp(Mathf.Repeat(transform.eulerAngles.y + 180, 360) - 180, -90, 90), 0);

        print(transform.eulerAngles);

    }

}
