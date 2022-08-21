using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.UI;

namespace WOW
{
    public class UIrotate_hiden : MonoBehaviour
    {
        //public GameObject mainCamera;
    
        // Update is called once per frame
        void Update()
        {
           transform.eulerAngles = -Camera.main.transform.eulerAngles;
        }
    }

}
