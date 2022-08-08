using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveFire : MonoBehaviour
{
    public Ballistic[] ballistics;
    public double shootAngle = 45;
    float x;
    float y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        x -= v;
        y += h;
        
        transform.rotation = Quaternion.Euler(x, y, 0);

        if (Input.GetButtonDown("Fire1"))
        {
            Ballistic bullet;
            foreach (Ballistic ballistic in ballistics)
            {
                bullet = Instantiate<Ballistic>(ballistic, transform.position, transform.rotation);
                //bullet.transform.position = transform.position;
                //bullet.transform.rotation = transform.rotation;
                //float angle = Vector3.SignedAngle(Vector3.up, transform.up, transform.forward);
                //bullet.OnShoot(angle);
/*
                bullet = Instantiate<TestBallistic>(ballistic);
                bullet.transform.position = transform.position;
                bullet.OnShoot((shootAngle - 15) * Mathf.Deg2Rad);

                bullet = Instantiate<TestBallistic>(ballistic);
                bullet.transform.position = transform.position;
                bullet.OnShoot((shootAngle + 15) * Mathf.Deg2Rad);*/
            }    
        }
    }
}
