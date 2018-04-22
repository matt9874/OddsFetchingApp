using MongoDB.Bson;
using System.Collections.Generic;

namespace OddsFetchingEntities
{
    public class Competition: Entity
    {
        public ObjectId _id { get; set; }

        public string TournamentId { get; set; }

        public string Region { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
