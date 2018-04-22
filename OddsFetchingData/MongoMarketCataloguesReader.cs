using DataInterfaces;
using MongoDB.Driver;
using OddsFetchingEntities;
using OddsFetchingEntities.Mappers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OddsFetchingData
{
    public class MongoMarketCataloguesReader : IIdsReader<MarketCatalogue>
    {
        private DataContext _context;
        private IMapper<MarketFilter, Expression<Func<MarketCatalogue, bool>>> _filterMapper;

        public MongoMarketCataloguesReader(IMapper<MarketFilter, Expression<Func<MarketCatalogue, bool>>> filterMapper)
        {
            _context = new DataContext();
            _filterMapper = filterMapper;
        }

        public ISet<string> ReadIds(MarketFilter filter)
        {
            Expression<Func<MarketCatalogue, bool>> filterExpression = _filterMapper.Map(filter);
            FilterDefinition<MarketCatalogue> mappedFilter = (FilterDefinition<MarketCatalogue>)(filterExpression);

            var collection = _context.GetCollection<MarketCatalogue>();

            HashSet<string> marketIds = new HashSet<string>();
            var x = collection.Find<MarketCatalogue>(mappedFilter);
            IEnumerable<MarketCatalogue> marketCatalogues = x.ToEnumerable<MarketCatalogue>();

            foreach (var markCat in marketCatalogues)
            {
                marketIds.Add(markCat.IdStr);
            }
            return marketIds;
        }
    }
}
