using System;
using System.Diagnostics;
using System.Timers;
using WorkerInterfaces;

namespace Workers
{
    public class TimedWorker: ITimedWorker
    {
        private IWorker _worker;
        private Timer _timer;
        private int _timeInterval;

        public string Name { get { return "Timed "+ _worker.Name; } }

        public TimedWorker(int timeInterval, IWorker worker)
        {
            _worker = worker;
            _timeInterval=timeInterval;
        }

        public void DoWork()
        {
            _timer = new System.Timers.Timer(_timeInterval);
            _timer.AutoReset = true;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(
                (s, a) => {
                    EventLog.WriteEntry("OddsFetchingTriggerer", _worker.Name + " about to do work");
                    _worker.DoWork();
                });
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
            _timer = null;
        }
    }
}
