using DataInterfaces;
using MongoDB.Driver;
using OddsFetchingEntities;

namespace OddsFetchingData
{
    public class MongoEventsSaver : ISaver<Event>
    {
        private DataContext _context;

        public MongoEventsSaver()
        {
            _context = new DataContext();
        }

        public void Save(Event ev)
        {
            var collection = _context.GetCollection<Competition>();
            var filter = Builders<Competition>.Filter.Eq(e => e.IdStr, ev.CompetitionId);
            var update = Builders<Competition>.Update.Push(e => e.Events, ev);
            collection.FindOneAndUpdate<Competition>(filter, update);
        }
    }
}
