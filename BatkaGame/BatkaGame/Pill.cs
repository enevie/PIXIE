using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatkaGame
{
    abstract class Pill : IDraw
    {
        int xCoord;
        int yCoord;
        char symbol;

        public int XCoord
        {
            get
            {
                return this.xCoord;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("xCoord must be >= 0");
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
                if (value < 0)
                {
                    throw new ArgumentException("yCoord must be >= 0");
                }
                else
                {
                    this.yCoord = value;
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

        public virtual void Draw()
        {
            Console.SetCursorPosition(this.XCoord, this.YCoord);
            Console.Write(this.Symbol);
        }

        public virtual void RespondToCollision(Batka batka)
        {
        }
    }
}
