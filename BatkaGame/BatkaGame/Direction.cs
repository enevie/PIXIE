using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatkaGame
{
    struct Direction
    {
        private int row;
        private int col;

        public Direction(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                this.row = value;
            }
        }

        public int Col
        {
            get
            {
                return this.col;
            }
            set
            {
                this.col = value;
            }
        }
    }
}
