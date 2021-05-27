namespace ChronometerApp
{
    class ChronometerEngine : IChronometerEngine
    {
        private readonly IChronometer _chronometer;
        private readonly ICommandReader _command;

        public ChronometerEngine(IChronometer chronometer, ICommandReader command)
        {
            _chronometer = chronometer;
            _command = command;
        }

        public void Run()
        {
            while (true)
            {
                _chronometer.SetCommand(_command.Read);
            }
        }
    }
}