using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraFire : MonoBehaviour
{
    public GameObject bullet;
    public float moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.position += new Vector3(h, v, 0) * moveSpeed * Time.deltaTime;

        float y = Input.GetAxis("Mouse ScrollWheel");

        transform.rotation =
            Quaternion.Euler(transform.rotation.eulerAngles.x + y * 1000 * Time.deltaTime, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bulletInstance = Instantiate(bullet, transform.position, transform.rotation);
            //bulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        }
    }
}
