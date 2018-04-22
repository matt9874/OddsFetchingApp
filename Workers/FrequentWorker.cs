using OddsFetchingService;
using WorkerInterfaces;

namespace Workers
{
    public class FrequentWorker : AbstractBetfairWorker, IWorker
    {
        public string Name { get { return "Frequent Worker"; } }

        public FrequentWorker(IBettingDataFetchingService service) : base(service)
        {

        }

        public override void DoWork()
        {
            _service.FetchAndStoreOdds();
        }
    }
}
