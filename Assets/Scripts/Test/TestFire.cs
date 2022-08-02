using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFire : MonoBehaviour
{
    public TestBallistic[] ballistics;
    public double shootAngle = 45;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            TestBallistic bullet;
            foreach (TestBallistic ballistic in ballistics)
            {
                bullet = Instantiate<TestBallistic>(ballistic);
                bullet.transform.position = transform.position;
                bullet.OnShoot(shootAngle * Mathf.Deg2Rad);
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
