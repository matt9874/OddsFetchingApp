using OddsFetchingEntities;
using System.Collections.Generic;

namespace OddsFetchingBLInterfaces
{
    public interface IMarketsService
    {
        void FetchAndStoreMarkets(MarketFilter filter, ISet<string> trackedMarketIds);
        void CloseFinishedMarkets(ISet<string> finishedEventIds);
        ISet<string> FetchTrackedMarketIds(MarketFilter filter);
    }
}
