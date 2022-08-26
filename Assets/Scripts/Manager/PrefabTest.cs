using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    
    private GameObject prefab;

    GameObject sound;

    void Start()
    {
        Managers.Resource.Instantiate("sound");
        Managers.Resource.Destory(sound);

    }
    
   
}
