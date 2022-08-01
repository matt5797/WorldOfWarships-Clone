using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TD : MonoBehaviour
{ 
    void Start()
    {
        shot();
    }

    public void shot()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);

        //Destroy(gameObject, 5);
        // 회전하여 총을 앞으로 던질 수 있어야한다
        // 정해진 지점에 곡선을 이루며 이동한다
        // Vector3 velo = Vector3.zero;
        //transform.position = Vector3.Lerp(firepos.transform.forward, target, 0.1f);


        // if (count >= 1)
        // {
        // 만약 정해진 지점에 도달 했다면 앞으로 직진한다
        // transform.Translate(Vector3.forward * 0.5f);
        // }
    }
}
