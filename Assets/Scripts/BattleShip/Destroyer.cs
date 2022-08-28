namespace WOW.BattleShip
{
    public class Destroyer : BattleShipBase
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            shipType = BattleShipType.Destroyer;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}