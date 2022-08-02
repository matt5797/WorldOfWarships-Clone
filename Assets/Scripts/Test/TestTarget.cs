using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW
{
    public class TestTarget : MonoBehaviour
    {
        public Controller.PlayerController playerController;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = playerController.TargetPoint;
        }
    }
}
