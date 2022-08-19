using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotate : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = Quaternion.LookRotation((target.transform.position - transform.position));
        transform.rotation = rot;
        //print(GetInstanceID() + " / " + rot.eulerAngles.y);

        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }
}
