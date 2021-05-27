namespace ChronometerApp.CommandModels
{
    using System;

    public class ExitCommand : ICommandModel
    {
        public void Execute(IChronometer chronometer)
        {
            Environment.Exit(0);
        }
    }
}