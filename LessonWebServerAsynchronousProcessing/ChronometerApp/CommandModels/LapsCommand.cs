namespace ChronometerApp.CommandModels
{
    using System;
    using System.Linq;

    public class LapsCommand : ICommandModel
    {
        public void Execute(IChronometer chronometer)
        {
            int i = 1;
            Console.WriteLine(chronometer.Laps.Count == 0
                ? "Laps: No laps"
                : "Laps:" + Environment.NewLine + string.Join(Environment.NewLine, chronometer.Laps.Select(x => $"{i++}. {x}")));
        }
    }
}