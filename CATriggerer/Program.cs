using StructureMap;
using System.Collections.Generic;
using System.Diagnostics;
using WorkerInterfaces;

namespace CATriggerer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            StructureMapConfig.RegisterComponents(container);
            
            IWorkerFactory _workerFactory = container.GetInstance<IWorkerFactory>();
            
            IList<string> _workerNames;
            IList<IWorker> _workers;
            int _numWorkers = 0; 
            
            _workerNames = _workerFactory.GetNames<IWorker>();
            _workers = new List<IWorker>();

            foreach (var name in _workerNames)
            {
                _workers.Add(_workerFactory.GetNamedWorker(name));
                _numWorkers++;
            }

            EventLog.WriteEntry("OddsFetchingTriggerer", "Started OddsFetchingTriggerer...");

            for (var i = 0; i < _numWorkers; i++)
            {
                _workers[i].DoWork();
            }
        }
        
    }
}
