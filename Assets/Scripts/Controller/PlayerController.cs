using UnityEngine;
using WOW.BattleShip;

namespace WOW.Controller
{
    public class PlayerController : MonoBehaviour
    {
        public BattleShipBase ship;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
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
        }
    }
}