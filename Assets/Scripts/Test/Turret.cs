using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private float yRotate;
    private float yRotateMove;
    public float rotateSpeed = 200.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed; 

        yRotate = yRotate + yRotateMove;
      
        yRotate = Mathf.Clamp(yRotate, -90, 90); // 위, 아래 고정

        Quaternion quat = Quaternion.Euler(new Vector3(0, yRotate, 0));
        transform.rotation = Quaternion.Slerp(transform.rotation, quat, Time.deltaTime);
    }
}
