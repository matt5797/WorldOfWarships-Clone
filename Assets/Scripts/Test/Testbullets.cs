using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testbullets : MonoBehaviour
{
    // �ؼ��鿡 ���� �������� �ʹ�
    // �ؼ��鿡 ������ ������ ���ư��� �ʹ�
    public float speed = 5;

    //��ǥ�� �޾ƿ��� ����
    public float torpedo_pos;
    public float water_pos;

    public Transform torpedo;
    public Transform water;



    void Update()
    {
        // ����� y���� �����Ѵ�
        torpedo_pos = torpedo.transform.position.y;

        if (GameObject.Find("torpedo Bullet"))
        {
            // ���� ��ڿ� ���� y ���� ���ٸ�
            if (transform.position.y <= 0)
            {
                
                // ��ڰ� ���� y���� ���� ���� ������ �ٵ��� y ���� �����ϰ�
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                // ��ڰ� ���� ���� �޷� ������
                transform.Translate(gameObject.transform.forward * Time.deltaTime, Space.World);
            }

        }
     
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.name == "Monster")
        {
            Debug.Log("�浹");
            Destroy(collision.gameObject); // ���װ�
            Destroy(gameObject); // ������
        }
    }
}
  
    


