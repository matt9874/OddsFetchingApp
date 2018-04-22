using DataInterfaces;
using OddsFetchingEntities;

namespace OddsFetchingData
{
    public class MongoOddsOffersSaver: ISaver<OddsOffer>
    {
        private DataContext _context;

        public MongoOddsOffersSaver()
        {
            _context = new DataContext();
        }

        public void Save(OddsOffer oddsOffer)
        {
            var collection = _context.GetCollection<OddsOffer>();
            collection.InsertOne(oddsOffer);
        }
    }
}
