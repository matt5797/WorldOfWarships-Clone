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
        // ȸ���Ͽ� ���� ������ ���� �� �־���Ѵ�
        // ������ ������ ��� �̷�� �̵��Ѵ�
        // Vector3 velo = Vector3.zero;
        //transform.position = Vector3.Lerp(firepos.transform.forward, target, 0.1f);


        // if (count >= 1)
        // {
        // ���� ������ ������ ���� �ߴٸ� ������ �����Ѵ�
        // transform.Translate(Vector3.forward * 0.5f);
        // }
    }
}
