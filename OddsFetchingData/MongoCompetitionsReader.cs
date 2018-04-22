using System.Collections.Generic;
using DataInterfaces;
using OddsFetchingEntities;
using MongoDB.Driver;
using System.Linq;

namespace OddsFetchingData
{
    public class MongoCompetitionsReader : IIdsReader<Competition>
    {
        private DataContext _context;

        public MongoCompetitionsReader()
        {
            _context = new DataContext();
        }

        public ISet<string> ReadIds(MarketFilter filter)
        {
            var compCollection = _context.GetCollection<Competition>();
            return new HashSet<string>(compCollection.Find(o => true).ToList().Select(comp => comp.IdStr));
        }
    }
}
