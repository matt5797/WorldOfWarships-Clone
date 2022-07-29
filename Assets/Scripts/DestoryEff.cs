using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryEff : MonoBehaviour
{
    public float destoryTime = 1.5f;
    float currentTime = 0;


    // Update is called once per frame
    void Update()
    {
        if(currentTime > destoryTime)
        {
            Destroy(gameObject);
        }
        currentTime += Time.deltaTime;
    }
}
