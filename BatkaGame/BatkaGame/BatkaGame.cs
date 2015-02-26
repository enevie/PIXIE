using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BatkaGame
{
    class BatkaGame
    {
        /*DONE!
        CLASS TIME IS NOT USED
        Changed movement logic.
        Generates pills.
        Collision detection.
        Destroy eated pills.
        Makes the БАТКА bigger every 2 seconds.
        If БАТКА is bigger than 5x5 GAME OVER!
        Score implemented. Every good pill gives 5 points. Every bad takes away 5 points.
        High score. Gets it from the file gamescore.txt
         DONE!*/

        //TODO: FIX: If БАТКА is on the rightmost column or lowest row and it grows after two seconds, he gets outside the boundaries of the console.

        public static int consoleHeight = Console.LargestWindowHeight;
        public static int consoleWidth = Console.LargestWindowWidth / 2;

        const string fileName = "../../gamescore.txt";

        public static Batka batka;
        public static List<Pill> pillsList = new List<Pill>();

        public static int timeToFat;
        public static int timer;
        public static int speed;
        public static bool isRunning;
        static void Main()
        {
            //Console.SetBufferSize(consoleWidth, consoleHeight);
            Console.SetWindowSize(consoleWidth - 1, consoleHeight - 1);

            int currentHighScore = GetHighScore();
            int currentScore = 0;

            GameMenu();

            //New thread for movement of БАТКА
            Thread threadMovement = new Thread(GeneratePills);
            threadMovement.Start();

            while (isRunning)
            {

                CountdownToFat(timer);
                timer += speed;

                Thread.Sleep(speed);
                MoveBatka();

                currentScore = Score(currentScore);

                Console.Clear();

                //If БАТКА bacame like Arnold? GAME OVER!
                CheckIfBatkaIsArnold();

                //Draw info
                DrawInfo(currentHighScore, currentScore);
                //Draw pill
                DrawPills();

                //Draw БАТКА
                batka.Draw();
            }

            Console.Clear();
            WriteHighScore(currentHighScore, currentScore);
            DrawInfo(currentHighScore, currentScore);
            GameScore(currentHighScore, currentScore);
            Console.SetCursorPosition(0, consoleHeight - 5);
            Console.Write(new string('-', consoleWidth - 1) + "\nPress any key to QUIT!\nPress ENTER to RESTART!");
            RestartOrQuit();


        }
        //Choose to restart or quit the game
        public static void RestartOrQuit()
        {
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Console.Clear();
                Main();
            }
            else if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                return;
            }
        }
        //Check if БАТКА is very big :)
        public static void CheckIfBatkaIsArnold()
        {
            if (batka.SideLength == 5)
            {
                isRunning = false;
                pillsList.Clear();
            }
        }

        //Time to get fat
        private static void CountdownToFat(int time)
        {
            if (time == timeToFat)
            {
                batka.MakeFat();
                timer = 0;
            }
        }

        //Draw pills
        private static void DrawPills()
        {
            List<Pill> buffer = new List<Pill>();

            buffer.AddRange(pillsList);

            foreach (var item in buffer)
            {
                item.Draw();
            }
        }

        //Calculate score
        private static int Score(int currentScore)
        {
            var pill = pillsList.FirstOrDefault(p => ((p.XCoord >= batka.XCoord && p.XCoord <= batka.XCoord + batka.SideLength - 1) && (p.YCoord >= batka.YCoord && p.YCoord <= batka.YCoord + batka.SideLength - 1)));

            if (pill != null)
            {
                pill.RespondToCollision(batka);
                if (pill is GoodPill)
                {
                    currentScore += 5;
                }
                if (pill is BadPill && currentScore > 0)
                {
                    currentScore -= 5;
                }
                pillsList.Remove(pill);
            }
            return currentScore;
        }

        //Draw info
        private static void DrawInfo(int currentHighScore, int currentScore)
        {
            Console.SetCursorPosition(1, 1);
            Console.Write("HIGH SCORE: {0}\n", currentHighScore);
            Console.Write(new string('-', consoleWidth));
            //Console.WriteLine(Console.LargestWindowHeight);
            //Console.WriteLine(Console.LargestWindowWidth);
            Console.SetCursorPosition(consoleWidth - 16, 1);
            Console.Write("YOUR SCORE: {0}\n", currentScore);
            Console.Write(new string('-', consoleWidth));


        }

        //Generate the pills
        private static void GeneratePills()
        {
            Random rand = new Random();
            while (isRunning)
            {
                //Thread for the generation of the pills
                Thread.Sleep(1000);
                var row = (int)rand.Next(0, consoleWidth - 1);
                var col = (int)rand.Next(3, consoleHeight - 1);

                var row1 = (int)rand.Next(0, consoleWidth - 1);
                var col1 = (int)rand.Next(3, consoleHeight - 1);

                BadPill badPill = new BadPill(row, col);
                GoodPill goodPill = new GoodPill(row1, col1);

                pillsList.Add(badPill);

                pillsList.Add(goodPill);
            }
        }

        // Creates menu of the game
        private static void GameMenu()
        {

            Console.TreatControlCAsInput = false;
            Console.CancelKeyPress += new ConsoleCancelEventHandler(BreakHandler);
            Console.Clear();
            Console.CursorVisible = false;

            string menuMessage = "Choose using down and up arrow keys and press enter";
            int xOffset = (consoleHeight - menuMessage.Length) / 2;
            int yOffset = consoleWidth / 3;
            Menu.WriteColorString(menuMessage, xOffset, yOffset, ConsoleColor.Black, ConsoleColor.White);
            string gameName = "   BATKA GAME   ";
            xOffset = (consoleHeight - gameName.Length) / 2;
            yOffset = consoleWidth / 10;
            Menu.WriteColorString(gameName, xOffset, yOffset, ConsoleColor.Black, ConsoleColor.White);
            string[] commands = { "Start", "Quit" };
            xOffset = (consoleHeight - commands[0].Length * 2) / 2;
            yOffset = consoleWidth / 6;
            int choice = Menu.ChooseComands(commands, xOffset, yOffset, ConsoleColor.Black, ConsoleColor.White);
            if (choice == 1)
            {
                Menu.CleanUp();
                Initiallize();
            }
            else
            {
                System.Environment.Exit(0);
            }
        }

        private static void BreakHandler(object sender, ConsoleCancelEventArgs args)
        {
            Menu.CleanUp();
        }

        private static void Initiallize()
        {
            isRunning = true;
            //Game speed
            speed = 50;
            //Time to become bigger in milliseconds
            timeToFat = 2000;
            //Initialize the timer
            timer = 0;
            CreateFile(fileName);
            batka = new Batka(consoleWidth / 2, consoleHeight / 2);
        }

        //Move the БАТКА
        private static void MoveBatka()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo userInput = Console.ReadKey();

                while (Console.KeyAvailable)
                {
                    userInput = Console.ReadKey();
                }

                if (userInput.Key == ConsoleKey.DownArrow && batka.YCoord < consoleHeight - 1)
                {
                    batka.YCoord++;
                }

                if (userInput.Key == ConsoleKey.UpArrow && batka.YCoord > 3)
                {
                    batka.YCoord--;
                }

                if (userInput.Key == ConsoleKey.LeftArrow && batka.XCoord > 0)
                {
                    batka.XCoord--;
                }

                if (userInput.Key == ConsoleKey.RightArrow && batka.XCoord < consoleWidth - 1)
                {
                    batka.XCoord++;
                }
            }
        }

        //Gets high score from the file
        public static int GetHighScore()
        {
            StreamReader reader = new StreamReader(@"../../gamescore.txt");

            string[] rawResults = reader.ReadToEnd().Split(new string[] { "\n", string.Empty }, StringSplitOptions.RemoveEmptyEntries);
            if (rawResults.Length == 0)
            {
                return 0;
            }
            int[] results = new int[rawResults.Length];

            for (int i = 0; i < results.Length; i++)
            {
                results[i] = int.Parse(rawResults[i]);
            }
            Array.Sort(results);
            Array.Reverse(results);
            reader.Close();
            int highScore = results[0];
            return highScore;

        }

        //Write high score in the file
        public static void WriteHighScore(int currentHighScore, int currentScore)
        {
            if (currentScore > currentHighScore)
            {
                var writer = new StreamWriter(@"../../gamescore.txt", true);
                using (writer)
                {
                    writer.WriteLine(currentScore);
                }
                writer.Close();
            }
        }

        //Final Game Score
        public static void GameScore(int currentHighScore, int currentScore)
        {


            Console.SetCursorPosition(0, consoleHeight / 3);
            Console.WriteLine(new string('-', consoleWidth));

            //Encoding encod = Encoding.GetEncoding("windows-1251");
            Encoding encod = Encoding.Unicode;
            Console.OutputEncoding = encod;
            if (currentScore == currentHighScore)
            {
                Console.WriteLine("ИМА И ДРУГИ БАТКИ КАТО ТЕБ!\n{0} points\n", currentScore);
            }
            else if (currentScore > currentHighScore)
            {
                Console.WriteLine("БРАО БЕ БАТКААААА!\nВЗИМАШ НАЦЕПИН ЯВНО!\nС ВКУС НА БАХУР!\nNew high score! {0} points\n", currentScore);
            }
            else
            {
                Console.WriteLine("СЛАБА БАТКА!\nResult: {0}\n", currentScore);
            }
            Console.WriteLine(new string('-', consoleWidth));
        }

        private static void CreateFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                using (File.Create(fileName)) { }
            }
        }
    }
}

