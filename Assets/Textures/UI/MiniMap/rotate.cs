using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.UI;

namespace WOW
{
    public class rotate : MonoBehaviour
    {
        public GameObject main;
    
        // Update is called once per frame
        void Update()
        {
           transform.rotation = main.transform.rotation;
        }
    }

}
