using System;

namespace ChronometerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ChronometerRun();
        }

        private static void ChronometerRun()
        {
            IChronometer chronometer = new Chronometer();

            while (true)
            {
                 Console.ReadLine().SetChronometerCommand(chronometer);
            }
        }
    }
}
