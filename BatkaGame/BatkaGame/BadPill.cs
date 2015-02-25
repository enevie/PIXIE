namespace BatkaGame
{
    class BadPill : Pill
    {
        public BadPill(int xCoord, int yCoord, char symbol = '$')
        {
            this.XCoord = xCoord;
            this.YCoord = yCoord;
            this.Symbol = symbol;
            
        }

        public override void RespondToCollision(Batka batka)
        {
            base.RespondToCollision(batka);   
            batka.MakeFat();
        }
    }
}
