namespace WOW.BattleShip
{
    public class AircraftCarrier : BattleShipBase
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            shipType = BattleShipType.AircraftCarrier;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}