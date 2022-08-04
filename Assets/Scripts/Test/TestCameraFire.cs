using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraFire : MonoBehaviour
{
    public GameObject bullet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.position += new Vector3(h, v, 0) * Time.time;

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        }
    }
}
