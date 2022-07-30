using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopedoFire : MonoBehaviour
{
    public Transform FirePos;

    public GameObject topedobullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
       Instantiate(topedobullet, FirePos.position, FirePos.rotation);
        
    }
}
