using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using WOW.Armament;

namespace WOW.Ability
{
    public class TorpedoTube : AbilityBase
    {
        [Header("Connections")]
        Armament.TorpedoTube[] armamentArray;
        
        [Header("Visuals")]

        [Header("Etc")]
        private bool m_IsOneClick = false;
        private double m_Timer = 0;
        
        private void Start()
        {
            armamentArray = GetComponentsInChildren<Armament.TorpedoTube>();
        }

        private void Update()
        {
            
        }

        public override void Ability()
        {
            OnShot();
        }

        public void OnShot()
        {
            foreach (Armament.TorpedoTube tube in armamentArray)
            {
                tube.TryFire();
            }
        }

        public override void OnAbilityChange()
        {
            
        }
    }
}