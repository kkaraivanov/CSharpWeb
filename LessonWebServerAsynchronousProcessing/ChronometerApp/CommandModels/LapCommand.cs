namespace ChronometerApp.CommandModels
{
    using System;

    public class LapCommand : ICommandModel
    {
        public void Execute(IChronometer chronometer)
        {
            var lap = chronometer.Lap();

            if (!string.IsNullOrEmpty(lap))
            {
                Console.WriteLine(lap);
            }
        }
    }
}