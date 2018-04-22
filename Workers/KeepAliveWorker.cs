using OddsFetchingService;
using WorkerInterfaces;

namespace Workers
{
    public class KeepAliveWorker : AbstractBetfairWorker, IWorker
    {
        public KeepAliveWorker(IBettingDataFetchingService service) : base(service)
        {
        }

        public string Name
        {
            get
            {
                return "Keep Alive Worker";
            }
        }

        public override void DoWork()
        {
            _service.KeepAlive();
        }
        
    }
}
