using UnityEngine;
using WOW.BattleShip;

namespace WOW.Controller
{
    public class PlayerController : MonoBehaviour
    {
        public BattleShipBase ship;

        //for double click
        public float m_DoubleClickSecond = 0.25f;
        private bool m_IsOneClick = false;
        private double m_Timer = 0;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // Ship Movement
            if (Input.GetKeyDown(KeyCode.W))
            {
                ship.GearUp();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                ship.GearDown();
            }
            if (Input.GetKey(KeyCode.A))
            {
                ship.SteerDown();
            }
            if (Input.GetKey(KeyCode.D))
            {
                ship.SteerUp();
            }

            // Ship Attack
            if (Input.GetMouseButtonDown(0))
            {
                if (!m_IsOneClick)
                {
                    m_Timer = Time.time;
                    m_IsOneClick = true;
                }
                else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
                {
                    m_IsOneClick = false;
                    ship.OnBurstShot();
                }
            }

            if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
            {
                ship.OnShot();
                m_IsOneClick = false;
            }

            // Skill Change
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ship.ChangeArmament(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ship.ChangeArmament(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ship.ChangeArmament(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ship.ChangeArmament(4);
            }
        }
    }
}