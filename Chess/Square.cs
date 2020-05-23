using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    struct Square
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public static Square NoneSquare = new Square(-1, -1);

        public Square(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Square(string e2)
        {
            if (e2.Length == 2 && e2[0] >= 'a' && e2[0] <= 'h' && e2[1] >= '1' && e2[1] <= '8')
            {
                x = e2[0] - 'a';
                y = e2[1] - '1';
            }
            else
                this = NoneSquare;
        }

        public bool onBoard()
        {
            return (x >= 0 && x < 8 && y >= 0 && y < 8);
        }
    }
}
