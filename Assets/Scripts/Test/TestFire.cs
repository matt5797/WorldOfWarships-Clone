using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFire : MonoBehaviour
{
    public TestBallistic[] ballistics;
    public double shootAngle = 0.5236;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            foreach (TestBallistic ballistic in ballistics)
            {
                ballistic.OnShoot(shootAngle);
            }    
        }
    }
}
