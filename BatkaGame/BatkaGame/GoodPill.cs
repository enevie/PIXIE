namespace BatkaGame
{
    class GoodPill : Pill
    {
        public GoodPill(int xCoord, int yCoord, char symbol = '@')
        {
            this.XCoord = xCoord;
            this.YCoord = yCoord;
            this.Symbol = symbol;
            this.Draw();
        }
    }
}
