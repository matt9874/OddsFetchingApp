using System;
using OddsFetchingService;
using WorkerInterfaces;

namespace Workers
{
    public class LoginWorker : AbstractBetfairWorker, IWorker
    {
        public string Name { get { return "Login Worker"; } }

        public LoginWorker(IBettingDataFetchingService service) : base(service)
        {
        }

        public override void DoWork()
        {
            _service.Login();
        }
    }
}
