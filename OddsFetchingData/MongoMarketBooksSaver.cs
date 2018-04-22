using DataInterfaces;
using OddsFetchingEntities;

namespace OddsFetchingData
{
    public class MongoMarketBooksSaver : ISaver<MarketBook>
    {
        private ISaver<Runner> _runnerSaver;

        public MongoMarketBooksSaver(ISaver<Runner> runnerSaver)
        {
            _runnerSaver = runnerSaver;
        }

        public void Save(MarketBook book)
        {
            var marketId = book.MarketId;
            var runners = book.Runners;
            foreach(var runner in runners)
            {
                runner.MarketId = marketId;
                _runnerSaver.Save(runner);
            }
        }
    }
}
