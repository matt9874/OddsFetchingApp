using System.Collections.Generic;
using OddsFetchingEntities;

namespace OddsFetchingBLInterfaces
{
    public interface IEventsService
    {
        ISet<string> FetchAndStoreEvents(MarketFilter filter, ISet<string> trackedEventIds);
        ISet<string> FetchTrackedEventIds(MarketFilter filter);
    }
}
