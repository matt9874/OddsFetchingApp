using MongoDB.Driver;
using OddsFetchingData.Properties;
using OddsFetchingEntities;
using System;
using System.Collections.Generic;

namespace OddsFetchingData
{
    public class DataContext
    {
        public readonly Dictionary<Type, string> classNameToDbCollectionNameMap = new Dictionary<Type, string>()
        {
            {typeof(Competition),"Competitions" },
            {typeof(MarketCatalogue),"MarketCatalogues" },
            {typeof(OddsOffer),"OddsOffers" }

        };

        public IMongoClient _client;
        public IMongoDatabase _db;
        
        public DataContext()
        {
            _client = new MongoClient(Settings.Default.MongoDbConnectionString);
            _db = _client.GetDatabase(Settings.Default.MongoDatabaseName, null);
        }

        private string GetCollectionName<T>()
        {
            Type collectionType = typeof(T);
            if (classNameToDbCollectionNameMap.ContainsKey(collectionType))
            {
                return classNameToDbCollectionNameMap[collectionType];
            }
            return null;
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _db.GetCollection<T>(GetCollectionName<T>());
        }
    }
}
