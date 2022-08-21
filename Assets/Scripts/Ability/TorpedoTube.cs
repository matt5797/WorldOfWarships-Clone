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
        TorpedoIndicator indicator;


        [Header("Visuals")]

        [Header("Etc")]
        private bool m_IsOneClick = false;
        private double m_Timer = 0;
        
        private void Start()
        {
            armamentArray = GetComponentsInChildren<Armament.TorpedoTube>();
            indicator = GetComponentInChildren<TorpedoIndicator>();
            if (indicator != null)
            {
                indicator.gameObject.SetActive(false);
            }
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

        public override void OnAbilityEnabled()
        {
            if (indicator != null)
            {
                indicator.gameObject.SetActive(true);
            }
        }

        public override void OnAbilityDisabled()
        {
            if (indicator != null)
            {
                indicator.gameObject.SetActive(false);
            }
        }
    }
}