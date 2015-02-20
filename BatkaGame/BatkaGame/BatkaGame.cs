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

        // new code from 20.02.2015 02:45
        public enum Directions { Right, Up, Left, Down }; // this Enum indcates directions of the move. Accept as an array[] - array[0]-Right, array[1]-Up, array[2]-Left, array[3]-Down
        public static Directions currentDirrection; // we'll need this in the game when batka have to move without arrows
        static void Main(string[] args)
        {
            // These are the directions
            // If we need to move Right, we have to increase the col, and the row have to be the same
            // If we need to move Up, we have to decrease the row, and the col have to be the same
            // If we need to move Left, we have to decrease the column, the row have to be the same
            // If we need to move Down, we have to increase the row, the col have to be the same
            Direction[] directionCoords = new Direction[]   
            {
                new Direction(0,1),
                new Direction(-1,0),
                new Direction(0,-1),
                new Direction(1,0)
            };
            GameMenu();
            /*
            Random rand = new Random();
            // creates all the objects we needed at the first initiallization - batka, badPill, goodPill
            Initiallize(rand);*/

            while (true)
            {
                // Checks for the user input and changes the coords of the bata. We pass as parameter the only one object batka, the cyrrent direction, and the struct where we hold all direcions coordinates
                MoveBatka(batka, currentDirrection, directionCoords);

                Console.Clear();
                
                // After Console.Clear() we have to draw the objects. Batka is the same one, the pills will be a lot. For now there is only one pill in each collection
                batka.Draw();
                GoodPillsDraw(goodPills);
                BadPillsDraw(badPills);
            }
        }

        private static void GameMenu()
        {
            //Console.SetBufferSize(ConsoleHeight, ConsoleWidth);
            Console.SetWindowSize(ConsoleHeight, ConsoleWidth);
            Console.TreatControlCAsInput = false;
            Console.CancelKeyPress += new ConsoleCancelEventHandler(BreakHandler);
            Console.Clear();
            Console.CursorVisible = false;
            Random rand = new Random();

            string[] comands = { "Start", "Quit" };
            Menu.WriteColorString("Choose using down and up arrow keys and press enter", 12, 20, ConsoleColor.Black, ConsoleColor.White);

            int choice = Menu.ChooseComands(comands, 34, 3, ConsoleColor.Black, ConsoleColor.White);
            if (choice == 1)
            {
                Menu.CleanUp();
                Initiallize(rand);
            }
            else
                System.Environment.Exit(0);
            
        }


        private static void BreakHandler(object sender, ConsoleCancelEventArgs args)
        {
            Menu.CleanUp();
        }

        private static void BadPillsDraw(LinkedList<BadPill> badPills)
        {
            foreach (var badPill in badPills)
            {
                badPill.Draw();
            }
        }

        private static void GoodPillsDraw(LinkedList<GoodPill> goodPills)
        {
            foreach (var goodPill in goodPills)
            {
                goodPill.Draw();
            }
        }

        private static void Initiallize(Random rand)
        {
            Console.SetWindowSize(100, 50);
            batka = new Batka(ConsoleWidth / 2, ConsoleHeight / 2);
            BadPill badPill = new BadPill(rand.Next(0, ConsoleWidth - 1), rand.Next(0, ConsoleHeight - 1));
            GoodPill goodPill = new GoodPill(rand.Next(0, ConsoleWidth - 1), rand.Next(0, ConsoleHeight - 1));
            badPills = new LinkedList<BadPill>();
            goodPills = new LinkedList<GoodPill>();
            badPills.AddFirst(badPill);
            goodPills.AddFirst(goodPill);
        }
        private static void MoveBatka(Batka myBatka, Directions currentDirrection, Direction[] directionCoords)
        {
            ConsoleKeyInfo userInput = Console.ReadKey();

            switch (userInput.Key)
            {
                case ConsoleKey.DownArrow:
                    currentDirrection = Directions.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    currentDirrection = Directions.Left;
                    break;
                case ConsoleKey.RightArrow:
                    currentDirrection = Directions.Right;
                    break;
                case ConsoleKey.UpArrow:
                    currentDirrection = Directions.Up;
                    break;
                default: throw new ArgumentException("Invalid key");
            }

            //  Takes from the array directionCoords the new direction. 
            // Here it will be used public enum Directions just for clarity - assume that Right==0, Up==1, Left==2, Down==3
            // and this is an usual array operation - taking a value by its index
            Direction nextDirection = directionCoords[(int)currentDirrection];

            // calculate the new coordinates of batka after the movement 
            myBatka.XCoord += nextDirection.Row; // now batka have new row(xCoord) position
            myBatka.YCoord += nextDirection.Col; // now batka have new col(yCoord) position
        }
    }
}
