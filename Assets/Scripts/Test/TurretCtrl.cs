using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    public Transform cameraArm;
   // public float rotSpeed = 0;
   // private RaycastHit hit;
    
    void Update()
    {
        Move();
        LookAround();


    }
 
    void Move()
    {
        Debug.DrawRay(cameraArm.forward, cameraArm.position, Color.red);

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

        transform.forward = lookForward;
        transform.position += moveDir * 0.01f * Time.deltaTime;

    }

    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if(x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
