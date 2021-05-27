namespace ChronometerApp.CommandModels
{
    public interface ICommandModel
    {
        void Execute(IChronometer chronometer);
    }
}