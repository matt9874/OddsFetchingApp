using OddsFetchingEntities;
using DataInterfaces;
using System.Collections.Generic;
using MongoDB.Driver;

namespace OddsFetchingData
{
    public class MongoMarketCataloguesUpdater : IUpdater<MarketCatalogue>
    {
        private DataContext _context;

        public MongoMarketCataloguesUpdater()
        {
            _context = new DataContext();
        }

        public void UpdateMany(ISet<string> ids, string propertyName, string newValue)
        {
            var collection = _context.GetCollection<MarketCatalogue>();
            var builder = Builders<MarketCatalogue>.Filter;
            var filter = builder.In("MarketId", ids);
            var update = Builders<MarketCatalogue>.Update.Set(propertyName, newValue);

            var result = collection.UpdateMany(filter, update);
        }
    }
}
