using System.ServiceProcess;
using System.Collections.Generic;
using WorkerInterfaces;

namespace OddsFetchingTriggerer
{
    public partial class OddsFetchingTriggerer : ServiceBase
    {
        private IWorkerFactory _workerFactory;
        private IList<string> _workerNames;
        private IList<IWorker> _workers;
        private int _numWorkers { get { return _workers.Count; } }
        
        public OddsFetchingTriggerer(IWorkerFactory workerFactory)
        {
            _workerFactory = workerFactory;
            _workerNames = _workerFactory.GetNames<IWorker>();
            _workers = new List<IWorker>();

            foreach(var name in _workerNames)
            {
                _workers.Add(_workerFactory.GetNamedWorker(name));
            }
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Started OddsFetchingTriggerer...");

            for (var i = 0; i < _numWorkers; i++)
            {
                _workers[i].DoWork();
            }
        }

        protected override void OnStop()
        {
            for (var i = 0; i < _numWorkers; i++)
            {
                var worker = _workers[i];
                if (worker is ITimedWorker)
                {
                    ITimedWorker timedWorker = (ITimedWorker)worker;
                    timedWorker.StopTimer();
                }
            }
        }
    }
}
