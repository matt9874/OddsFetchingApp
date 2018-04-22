using DataInterfaces;
using OddsFetchingBLInterfaces;
using OddsFetchingEntities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace OddsFetchingBL
{
    public class EventsService: IEventsService
    {
        private IReader<EventResult> _eventsReader;
        private IIdsReader<Event> _trackedEventsReader;
        private ISaver<Event> _eventsSaver;

        public EventsService(IReader<EventResult> eventsReader, IIdsReader<Event> trackedEventsReader, ISaver<Event> eventsSaver)
        {
            _eventsReader = eventsReader;
            _trackedEventsReader = trackedEventsReader;
            _eventsSaver = eventsSaver;
        }

        public ISet<string> FetchAndStoreEvents(MarketFilter filter, ISet<string> trackedEventIds)
        {
            IEnumerable<Event> events = new List<Event>();
            try
            {
                events = EventResultsConverter.ConvertEventResults(_eventsReader.Read(filter));
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("OddsFetchingTriggerer", "Error fetching events: " + e.Message);
                trackedEventIds = new HashSet<string>();
            }
            
            List<string> untrackedEventIds = new List<string>();
            HashSet<string> eventIds = new HashSet<string>();

            foreach (Event ev in events)
            {
                ev.CompetitionId = filter.CompetitionIds.FirstOrDefault();
                ev.Markets = new List<MarketCatalogue>();

                eventIds.Add(ev.IdStr);

                if (!trackedEventIds.Contains(ev.IdStr))
                {
                    untrackedEventIds.Add(ev.IdStr);
                    _eventsSaver.Save(ev);
                }
            }
            return eventIds;
        }
        public ISet<string> FetchTrackedEventIds(MarketFilter filter)
        {
            ISet<string> trackedEventIds;
            try
            {
                trackedEventIds = _trackedEventsReader.ReadIds(filter);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("OddsFetchingTriggerer", "Error reading tracked event ids " + e.Message);
                throw;
            }
            return trackedEventIds;
        }
    }
}
