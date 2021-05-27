namespace ChronometerApp
{
    using System;
    using System.Linq;

    public static class ChronometerCommands
    {
        public static void SetCommand(this IChronometer chronometer, string command)
        {
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
                    int i = 1;
                    Console.WriteLine(chronometer.Laps.Count == 0
                        ? "Laps: No laps"
                        : "Laps:" + Environment.NewLine + string.Join(Environment.NewLine, chronometer.Laps.Select(x => $"{i++}. {x}")));
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
                    Environment.Exit(0);
                    break;
            }
        }
    }
}