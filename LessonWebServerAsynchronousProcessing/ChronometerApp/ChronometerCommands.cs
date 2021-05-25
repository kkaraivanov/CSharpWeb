namespace ChronometerApp
{
    using System;

    public static class ChronometerCommands
    {
        public static void SetChronometerCommand(this IChronometer chronometer, string command)
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