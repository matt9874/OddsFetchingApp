using OddsFetchingService;
using WorkerInterfaces;

namespace Workers
{
    public class DailyWorker : AbstractBetfairWorker, IWorker
    {
        public string Name { get { return "Daily Worker"; } }

        public DailyWorker(IBettingDataFetchingService service) : base(service)
        {

        }

        public override void DoWork()
        {
            _service.UpdateStatusOfRecentlyFinishedEntities();
            _service.FetchAndStoreUntrackedEntities();
        }
    }
}
