using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testfire1 : MonoBehaviour
{
    public GameObject prefabs;
    public Transform firepos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(prefabs, firepos.transform.position, firepos.transform.rotation);
        }
    }
}
