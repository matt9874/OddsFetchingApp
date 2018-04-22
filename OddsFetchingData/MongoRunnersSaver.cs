using DataInterfaces;
using OddsFetchingEntities;

namespace OddsFetchingData
{
    public class MongoRunnersSaver : ISaver<Runner>
    {
        private ISaver<OddsOffer> _oddsOfferSaver;

        public MongoRunnersSaver(ISaver<OddsOffer> oddsOfferSaver)
        {
            _oddsOfferSaver = oddsOfferSaver;
        }

        public void Save(Runner runner)
        {
            _oddsOfferSaver.Save(runner.BestBackOffer);
            _oddsOfferSaver.Save(runner.BestLayOffer);
        }
    }
}
