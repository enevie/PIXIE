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
        public static LinkedList<BadPill> badPills;
        public static LinkedList<GoodPill> goodPills;
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
            BadPill badPill = new BadPill(rand.Next(0, ConsoleWidth - 1), rand.Next(0, ConsoleHeight - 1));
            GoodPill goodPill = new GoodPill(rand.Next(0, ConsoleWidth - 1), rand.Next(0, ConsoleHeight - 1));
            badPills = new LinkedList<BadPill>();
            goodPills = new LinkedList<GoodPill>();
            badPills.AddFirst(badPill);
            goodPills.AddFirst(goodPill);
        }
    }
}
