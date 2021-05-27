namespace ChronometerApp.CommandModels
{
    public class ResetCommand : ICommandModel
    {
        public void Execute(IChronometer chronometer)
        {
            chronometer.Reset();
        }
    }
}