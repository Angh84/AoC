using System.Diagnostics;

namespace AoC.Core.Utils
{
    public class PerformanceTracker
    {
        private readonly Stopwatch _stopwatch = new();

        public void Start() => _stopwatch.Start();
        public void Stop() => _stopwatch.Stop();
        public void Reset() => _stopwatch.Reset();

        public double ElapsedMilliseconds => _stopwatch.Elapsed.TotalMilliseconds;
        public double ElapsedSeconds => _stopwatch.Elapsed.TotalSeconds;
    }
}