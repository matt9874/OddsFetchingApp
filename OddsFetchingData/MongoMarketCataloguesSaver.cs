using DataInterfaces;
using OddsFetchingEntities;

namespace OddsFetchingData
{
    public class MongoMarketCataloguesSaver: ISaver<MarketCatalogue>
    {
        private DataContext _context;

        public MongoMarketCataloguesSaver()
        {
            _context = new DataContext();
        }

        public void Save(MarketCatalogue marketCatalogue)
        {
            var collection = _context.GetCollection<MarketCatalogue>();
            collection.InsertOne(marketCatalogue);
        }
    }
}
