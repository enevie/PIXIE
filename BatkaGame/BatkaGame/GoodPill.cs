namespace BatkaGame
{
    class GoodPill : Pill
    {
        public GoodPill(int xCoord, int yCoord, char symbol = '@')
        {
            this.XCoord = xCoord;
            this.YCoord = yCoord;
            this.Symbol = symbol;
            
        }

        public override void RespondToCollision(Batka batka)
        {
            base.RespondToCollision(batka);
            batka.MakeSlim();

        }
    }
}
