namespace ChronometerApp.CommandModels
{
    using System;

    public class TimeCommand : ICommandModel
    {
        public void Execute(IChronometer chronometer)
        {
            var time = chronometer.GetTime;
            if (!string.IsNullOrEmpty(time))
            {
                Console.WriteLine(time);
            }
        }
    }
}