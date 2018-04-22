using DataInterfaces;
using OddsFetchingBLInterfaces;
using OddsFetchingEntities;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace OddsFetchingBL
{
    public class MarketsService: IMarketsService
    {
        private IReader<MarketCatalogue> _marketsReader;
        private IIdsReader<MarketCatalogue> _trackedMarketsReader;
        private ISaver<MarketCatalogue> _marketsSaver;
        private IUpdater<MarketCatalogue> _marketsUpdater;

        public MarketsService(IReader<MarketCatalogue> marketsReader, IIdsReader<MarketCatalogue> trackedMarketsReader,
                              ISaver<MarketCatalogue> marketsSaver, IUpdater<MarketCatalogue> marketsUpdater)
        {
            _marketsReader = marketsReader;
            _trackedMarketsReader = trackedMarketsReader;
            _marketsSaver = marketsSaver;
            _marketsUpdater = marketsUpdater;
        }

        public void FetchAndStoreMarkets(MarketFilter filter, ISet<string> trackedMarketIds)
        {
            AddMarketTypesToFilter(filter);

            IEnumerable<MarketCatalogue> markets;
            try
            {
                markets = _marketsReader.Read(filter);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("OddsFetchingTriggerer", "Error fetching markets: " + e.Message);
                markets = new HashSet<MarketCatalogue>();
            }
            

            List<string> untrackedMarketIds = new List<string>();

            foreach (MarketCatalogue market in markets)
            {
                if (!trackedMarketIds.Contains(market.MarketId))
                {
                    market.EventId = filter.EventIds.FirstOrDefault();

                    untrackedMarketIds.Add(market.MarketId);
                    _marketsSaver.Save(market);
                }
            }
        }
        
        public ISet<string> FetchTrackedMarketIds(MarketFilter filter)
        {
            ISet<string> trackedMarketIds;
            try
            {
                trackedMarketIds = _trackedMarketsReader.ReadIds(filter);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("OddsFetchingTriggerer", "Error reading tracked marketids: " + e.Message);
                throw;
            }
            return trackedMarketIds;
        }
        
        private void AddMarketTypesToFilter(MarketFilter filter)
        {
            filter.MarketTypeCodes = new HashSet<string>();
            var allowedCodes = Enum.GetValues(typeof(AllowedMarketTypeCodes)).Cast<AllowedMarketTypeCodes>();
            foreach(var code in allowedCodes)
            {
                filter.MarketTypeCodes.Add(code.ToString());
            }
        }

        public void CloseFinishedMarkets(ISet<string> finishedMarketIds)
        {
            _marketsUpdater.UpdateMany(finishedMarketIds, "IsFinished", "true");
        }
    }
}
