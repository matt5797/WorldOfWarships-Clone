using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

using WOW.Armament;

namespace WOW.Ability
{
    public class MainBatteryAP : AbilityBase
    {
        [Header("Connections")]
        MainBattery[] armamentArray;
        
        [Header("Visuals")]

        [Header("Etc")]
        //for double click
        public float m_DoubleClickSecond = 0.25f;
        private bool m_IsOneClick = false;
        private float m_Timer = 0;
        
        private void Start()
        {
            armamentArray = GetComponentsInChildren<MainBattery>();
        }

        private void Update()
        {
            if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
            {
                OnShot();
                m_IsOneClick = false;
            }
        }

        public override void Ability()
        {
            if (!m_IsOneClick)
            {
                m_Timer = Time.time;
                m_IsOneClick = true;
            }
            else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
            {
                m_IsOneClick = false;
                OnBurstShot();
            }
        }

        public void OnShot()
        {
            foreach (MainBattery battery in armamentArray)
            {
                if (battery.TryFire())
                {
                    return;
                }
            }
        }

        public void OnBurstShot()
        {
            foreach (MainBattery battery in armamentArray)
            {
                battery.TryFire();
            }
        }

        public override void OnAbilityEnabled()
        {
            foreach (MainBattery battery in armamentArray)
            {
                battery.ChangeBullet("AP");
            }
        }
    }
}