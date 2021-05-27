namespace ChronometerApp.CommandModels
{
    public class StopCommand : ICommandModel
    {
        public void Execute(IChronometer chronometer)
        {
            chronometer.Stop();
        }
    }
}