using System;
using System.Collections.Generic;
using OddsFetchingBLInterfaces;
using OddsFetchingEntities;
using System.Linq;
using OddsFetchingEntities.Extensions;
using DataInterfaces;

namespace OddsFetchingBL
{
    public class MarketBooksService : IMarketBooksService
    {
        private readonly int _maxMarketsPerListMarketBookRequest = 40;

        private IReader<MarketBook> _oddsReader;
        private ISaver<MarketBook> _oddsSaver;

        public MarketBooksService(IReader<MarketBook> oddsReader, ISaver<MarketBook> oddsSaver)
        {
            _oddsReader = oddsReader;
            _oddsSaver = oddsSaver;
        }

        public void FetchAndStoreOdds(IEnumerable<string> marketIds)
        {
            int numIds = marketIds.Count();

            if (numIds <= _maxMarketsPerListMarketBookRequest)
            {
                IEnumerable<MarketBook> fetchedOdds = _oddsReader.Read(new MarketFilter() { MarketIds = new HashSet<string>(marketIds) });
                if (!fetchedOdds.IsNullOrEmpty())
                {
                    foreach (var book in fetchedOdds)
                    {
                        TimeStamp(book);
                        _oddsSaver.Save(book);
                    }
                }
            }
            else
            {
                IEnumerable<IEnumerable<string>> batchesOfIds = marketIds.Split((numIds / _maxMarketsPerListMarketBookRequest) + 1);
                foreach (var batch in batchesOfIds)
                {
                    FetchAndStoreOdds(batch);
                }
            }
        }
        
        private void TimeStamp(MarketBook book)
        {
            var runners = book.Runners;
            var timeStamp = DateTime.Now;

            foreach (var runner in runners)
            {
                runner.BestBackOffer.OfferDateTime = timeStamp;
                runner.BestLayOffer.OfferDateTime = timeStamp;
            }
        }

        public ISet<string> FetchClosedMarketIds(ISet<string> ids)
        {
            var closedMarketIds = new HashSet<string>();

            foreach (var id in ids)
            {
                var marketBooks = _oddsReader.Read(new MarketFilter() { MarketIds = new HashSet<string>() { id } });
                var marketBook = marketBooks.FirstOrDefault<MarketBook>(o => o.Status == MarketStatus.CLOSED);
                if (marketBook != null)
                {
                    closedMarketIds.Add(id);
                }
            }
            return closedMarketIds;
        }
    }
}
