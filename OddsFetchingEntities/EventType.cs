using System.Collections.Generic;

namespace OddsFetchingEntities
{
    public class EventType:Entity
    {
        public IEnumerable<Competition> Competitions { get; set; }
    }
}
