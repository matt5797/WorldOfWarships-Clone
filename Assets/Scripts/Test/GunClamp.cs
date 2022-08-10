using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunClamp : MonoBehaviour
{
    public enum GunSet
    {
        front,
        back
    }

    public GunSet gunSet = GunSet.front;


    public Transform root;
    public Vector3 target;
    [Range(0.0f, 1.0f)]
    public float rotSpeed = 0.8f;

    void Start()
    {
        //transform.rotation = gunSet == GunSet.front ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(0, 180, 0);
    }

    void Update()
    {
        target = GetMouseWorldPosition();
        target.y = transform.position.y;

        Vector3 dir = (target - transform.position).normalized;

        float frontBack = Vector3.Dot(root.forward, dir);


        if (gunSet == GunSet.front && frontBack >= 0)
        {
            //transform.rotation = Quaternion.LookRotation(dir);
            Quaternion rot = Quaternion.LookRotation(dir, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed);
        }
        else if(gunSet == GunSet.back && frontBack < 0)
        {
            //transform.rotation = Quaternion.LookRotation(dir);
            Quaternion rot = Quaternion.LookRotation(dir * -1, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed);
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            return hitInfo.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (target != Vector3.zero)
        {
            Gizmos.DrawSphere(target, 1.0f);
        }
    }
}
