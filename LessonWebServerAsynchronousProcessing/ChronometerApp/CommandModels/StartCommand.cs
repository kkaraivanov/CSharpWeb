namespace ChronometerApp.CommandModels
{
    public class StartCommand : ICommandModel
    {
        public void Execute(IChronometer chronometer)
        {
            chronometer.Start();
        }
    }
}