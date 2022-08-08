using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testbullets : MonoBehaviour
{

    public float speed = 5;

    //��ǥ�� �޾ƿ��� ����
    public int torpedo_pos;
    public int monster_pos;

    public Transform torpedo;
    public Transform monster;


    void Update()
    {
        // ����� y���� �����Ѵ�
        torpedo_pos = (int)torpedo.transform.position.y;

        if (GameObject.Find("torpedo Bullet"))
        {
            // ���͸� ã�� y���� ���� �Ѵ�
            monster_pos = (int)GameObject.Find("Monster").transform.position.y;

            // ���� ��ڿ� ������ y ���� ���ٸ�
            if (torpedo_pos == monster_pos)
            {
                
                // ��ڰ� ���Ϳ��� y���� ���� ���� ������ �ٵ��� y ���� �����ϰ�
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                // ��ڰ� ���͸� ���� �޷� ������
                transform.position = Vector3.MoveTowards(gameObject.transform.position, monster.transform.position, speed * Time.deltaTime);
               
            }

        }
        else
        {
            // ���� ���Ͱ� ���� �� �׳� �ٴڿ� ��������
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
  
    


