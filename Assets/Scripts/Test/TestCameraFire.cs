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

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            //RaycastHit hit;
            
            RaycastHit[] hit = Physics.RaycastAll(transform.position, transform.forward, 100, ~0, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < hit.Length; i++)
            {
                Debug.Log(hit[i].collider.name +" / "+ hit[i].point + " / " + hit[i].normal);
            }

            Physics.Linecast(transform.position, transform.forward * 100);
        }
    }
}
