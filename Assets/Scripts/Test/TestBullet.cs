using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TestBallistic))]
public class TestBullet : MonoBehaviour
{
    private TestBallistic ballistic;
    
    // Start is called before the first frame update
    void Start()
    {
        ballistic = GetComponent<TestBallistic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        double penetration = ballistic.GetPenetration();
        double impactAngle = ballistic.GetImpactAngle();
        
        print(gameObject.name + "관통력: " + penetration + " / 입사각" + impactAngle);
    }
}
