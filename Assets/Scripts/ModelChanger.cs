using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelChanger : MonoBehaviour
{
    public GameObject[] models;
    int currentModel = 5;

    // Start is called before the first frame update
    void Start()
    {
        models[currentModel].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeModel(int index)
    {
        print(index);
        models[index].SetActive(true);
        models[currentModel].SetActive(false);
        currentModel = index;
    }
}
