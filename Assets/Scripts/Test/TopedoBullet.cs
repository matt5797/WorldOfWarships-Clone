using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopedoBullet : MonoBehaviour
{
    public float speed = 3f;
    public GameObject targetPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos.transform.position, speed * Time.deltaTime);

    }
}
