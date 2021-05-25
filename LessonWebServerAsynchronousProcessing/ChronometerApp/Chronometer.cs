namespace ChronometerApp
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Chronometer : IChronometer
    {
        private readonly Stopwatch _stopwatch;
        private bool _isTrue;

        public Chronometer()
        {
            Laps = new List<string>();
            _stopwatch = new Stopwatch();
        }

        public string GetTime => getTime();

        public List<string> Laps { get; }

        public string Lap()
        {
            if (_isTrue)
            {
                var lapTime = GetTime;
                Laps.Add(lapTime);

                return lapTime;
            }

            return string.Empty;
        }

        public void Reset()
        {
            _stopwatch.Reset();
            _isTrue = false;
            Laps.RemoveRange(0, Laps.Count);
            
        }

        public void Start()
        {
            _stopwatch.Start();
            _isTrue = true;
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }

        private string getTime()
        {
            if (_isTrue)
            {
                var ts = _stopwatch.Elapsed;
                return string.Format("{0:00}:{1:00}.{2:0000}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            }

            return string.Empty;
        }
    }
}