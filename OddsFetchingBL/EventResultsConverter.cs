using OddsFetchingEntities;
using System.Collections.Generic;

namespace OddsFetchingBL
{
    public class EventResultsConverter
    {
        public static IEnumerable<Event> ConvertEventResults(IEnumerable<EventResult> eventResults)
        {
            var events = new List<Event>();

            foreach (EventResult evRes in eventResults)
            {
                events.Add(evRes.Event);
            }

            return events;
        }
    }
}
