using System;

namespace BatkaGame
{
    class Batka
    {
        public static int consoleHeight = Console.LargestWindowHeight;
        public static int consoleWidth = Console.LargestWindowWidth / 2;
        private const int InitialLength = 2;
        private int xCoord;
        private int yCoord;
        private int sideLength;
        private char symbol;

        public Batka(int xCoord, int yCoord, int sideLength = InitialLength, char symbol = '#')
        {
            this.XCoord = xCoord;
            this.YCoord = yCoord;
            this.SideLength = sideLength;
            this.Symbol = symbol;
            this.Draw();
        }
        public int XCoord
        {
            get
            {
                return this.xCoord;
            }
            set
            {
                if (value < -1)
                {
                    throw new ArgumentException("xCoord must be >= 0");
                }
                else if (value > consoleWidth - 1 - sideLength)
                {
                    this.xCoord = value - sideLength;
                }
                else
                {
                    this.xCoord = value;
                }

            }
        }
        public int YCoord
        {
            get
            {
                return this.yCoord;
            }
            set
            {
                if (value < -1)
                {
                    throw new ArgumentException("yCoord must be >= 0");
                }
                else if (value > consoleHeight - 1 - sideLength)
                {
                    this.yCoord = value - sideLength;
                }
                else 
                {
                    this.yCoord = value;
                }

            }
        }
        public int SideLength
        {
            get
            {
                return this.sideLength;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("SideLength must be > 0");
                }
                else
                {
                    this.sideLength = value;
                }

            }
        }
        public char Symbol
        {
            get
            {
                return this.symbol;
            }
            set
            {
                this.symbol = value;
            }
        }
        public void Draw()
        {
            for (int row = 0; row < this.SideLength; row++)
            {
                Console.SetCursorPosition(this.XCoord, this.YCoord + row);

                for (int col = 0; col < this.SideLength; col++)
                {
                    Console.SetCursorPosition(this.XCoord + col, this.YCoord + row);
                    Console.Write(this.Symbol);
                    
                }
            }
        }
        public void MakeFat()
        {
            if (consoleWidth - 1 - sideLength > xCoord)
            {
                xCoord--;
            }
            if (consoleHeight - 1 - sideLength > yCoord)
            {
                yCoord--;
            }
            this.SideLength++;
        }
        public void MakeSlim()
        {
            if (this.SideLength > InitialLength)
            {
                this.SideLength--;
            }
        }
    }
}
