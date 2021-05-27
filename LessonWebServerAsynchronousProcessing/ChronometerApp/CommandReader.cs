namespace ChronometerApp
{
    using System;

    class CommandReader : ICommandReader
    {
        public string Read => Console.ReadLine();
    }
}