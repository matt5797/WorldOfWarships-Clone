using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Data;

namespace WOW.Projectile
{
    public class Torpedo : ProjectileBase
    {
        public TorpedoData torpedoData;
        
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
            // 피격 시 이벤트
        }
    }
}