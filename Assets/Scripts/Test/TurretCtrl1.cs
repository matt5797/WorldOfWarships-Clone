using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl1 : MonoBehaviour
{
    // 목적지
    public Transform target; 
    // x 회전값
    public float rotY = -270;
    // 회전 스피드
    public float Speed = 1;
    //public float rotSpeed = 10;
    float Angle;
    void Update()
    {

        // 목적지 = 조준점
        // 타겟과 나의 거리
        Vector3 targetDirection = target.position - transform.position;

        float singleStep = Speed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        //타겟을 바라보며 회전해라
        transform.rotation = Quaternion.LookRotation(newDirection);

        Angle = Mathf.Repeat(Angle + 180, 360) - 180;

        transform.eulerAngles = new Vector3(0, Mathf.Clamp(Mathf.Repeat(transform.eulerAngles.y + 180, 360) - 180, -90, 90), 0);

        print(transform.eulerAngles);

    }

}
