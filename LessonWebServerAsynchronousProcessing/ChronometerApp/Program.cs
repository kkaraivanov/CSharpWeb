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
                var command = Console.ReadLine();

                switch (command)
                {
                    case "start":
                        chronometer.Start();
                        break;
                    case "stop":
                        chronometer.Stop();
                        break;
                    case "lap":
                        var lap = chronometer.Lap();

                        if (!string.IsNullOrEmpty(lap))
                        {
                            Console.WriteLine(lap);
                        }

                        break;
                    case "laps":
                        Console.WriteLine(chronometer.Laps.Count == 0
                            ? "Laps: No laps"
                            : "Laps:" + Environment.NewLine + string.Join(Environment.NewLine, chronometer.Laps));
                        break;
                    case "time":
                        var time = chronometer.GetTime;
                        if (!string.IsNullOrEmpty(time))
                        {
                            Console.WriteLine(time);
                        }
                        break;
                    case "reset":
                        chronometer.Reset();
                        break;
                    case "exit":
                        return;
                        break;
                }
            }
        }
    }
}
