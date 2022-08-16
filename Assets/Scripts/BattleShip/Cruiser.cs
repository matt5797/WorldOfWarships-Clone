namespace WOW.BattleShip
{
    public class Cruiser : BattleShipBase
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            shipType = BattleShipType.Cruiser;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}