using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Data;

namespace WOW.Projectile
{
    public class Shell : ProjectileBase
    {
        public ShellData shellData;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        protected override void OnImpact()
        {
            // �ǰ� �� �̺�Ʈ
            print("Shell OnImpact");
        }
    }
}