using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition;
    public GameObject HE_Factory;
    public float throwPower = 0;
    // public ParticleSystem bulletImpact;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {

                GameObject highExplosive = Instantiate(HE_Factory);
                highExplosive.transform.position = firePosition.transform.position;

                Rigidbody rb = highExplosive.GetComponent<Rigidbody>();
                rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);

            }


        }
    }
}
