using System.Collections.Generic;
using DataInterfaces;
using OddsFetchingEntities;
using MongoDB.Driver;

namespace OddsFetchingData
{
    public class MongoEventsReader : IIdsReader<Event>
    {
        private DataContext _context;

        public MongoEventsReader()
        {
            _context = new DataContext();
        }

        public ISet<string> ReadIds(MarketFilter filter)
        {
            ISet<string> compIds = filter.CompetitionIds;
            IMongoCollection<Competition> collection = _context.GetCollection<Competition>();
            FilterDefinition<Competition> compsFilter = Builders<Competition>.Filter.In(x => x.IdStr, compIds);
            IEnumerable<Competition> comps = collection.Find(compsFilter).ToList();

            ISet<string> ids = new HashSet<string>();

            foreach (var comp in comps)
            {
                foreach (var ev in comp.Events)
                {
                    ids.Add(ev.IdStr);
                }
            }
            return ids;
        }
    }
}
