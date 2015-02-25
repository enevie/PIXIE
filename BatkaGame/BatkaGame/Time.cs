using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace BatkaGame
{
    //CLASS TIME IS NOT USED
    class Time
    {
        private static Thread thread;
        private static int StartStop = 0;
        public static int Hours = 0;
        public static int Minutes = 0;
        public static int Seconds = 0;
        public static int Milliseconds = 0;

        public static void StopTimer()
        {
            StartStop = 1;
            Console.WriteLine(Minutes);
        }
        private static void Timer()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (StartStop != 1)
            {
                TimeSpan ts = stopWatch.Elapsed;
                Hours = ts.Hours;
                Minutes = ts.Minutes;
                Seconds = ts.Seconds;
                Milliseconds = ts.Milliseconds;
                if (StartStop == 1)
                {
                    stopWatch.Stop();
                }

            }
        }

        public static void StartTimer()
        {
            thread = new Thread(Timer);
            thread.Start();
        }
    }
}