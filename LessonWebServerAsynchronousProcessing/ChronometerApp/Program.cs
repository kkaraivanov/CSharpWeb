using System;

namespace ChronometerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ChronometerRun();
        }

        private static void ChronometerRun()
        {
            IChronometer chronometer = new Chronometer();
            ICommandReader reader = new CommandReader();
            IChronometerEngine engine = new ChronometerEngine(chronometer, reader);
            engine.Run();
        }
    }
}
