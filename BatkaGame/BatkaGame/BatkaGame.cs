using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatkaGame
{
    class BatkaGame
    {

        const int ConsoleHeight = 100;
        const int ConsoleWidth = 50;
        public static Batka batka;
        static void Main(string[] args)
        {

            Random rand = new Random();
            Initiallize(rand);


            //while (true)
            //{



            //}
        }

        private static void Initiallize(Random rand)
        {
            batka = new Batka(ConsoleWidth / 2, ConsoleHeight / 2);           
        }
    }
}
