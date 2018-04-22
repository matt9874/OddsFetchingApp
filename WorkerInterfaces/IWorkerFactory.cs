using System.Collections.Generic;

namespace WorkerInterfaces
{
    public interface IWorkerFactory
    {
        IList<string> GetNames<TService>();
        IWorker CreateWorker();
        IWorker GetNamedWorker(string workerName);
    }
}
