using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace WOW.Ability
{
    public class MainBattery : AbilityBase
    {
        [Header("Connections")]
        [SerializeField] private Transform rootPosition = default;
        [Header("Visuals")]
        [SerializeField] private ParticleSystem slashParticles = default;

        private void Start()
        {
            
        }

        public override void Ability()
        {
            
        }
    }
}