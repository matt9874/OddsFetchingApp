using OddsFetchingService;

namespace Workers
{
    public abstract class AbstractBetfairWorker 
    {
        protected IBettingDataFetchingService _service;
        public AbstractBetfairWorker(IBettingDataFetchingService service)
        {
            _service = service;
        }

        public abstract void DoWork();
    }
}