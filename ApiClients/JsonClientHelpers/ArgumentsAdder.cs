using OddsFetchingEntities;
using System.Collections.Generic;

namespace ApiClients.JsonClientHelpers
{
    internal class ArgumentsAdder<T>
    {
        private static readonly string FILTER = "filter";
        private static readonly string MAX_RESULTS = "maxResults";
        private static readonly string MARKET_PROJECTION = "marketProjection";
        private static readonly string MARKET_IDS = "marketIds";
        private static readonly string PRICE_PROJECTION = "priceProjection";

        private static readonly HashSet<MarketProjection> _marketProjection = new HashSet<MarketProjection>() { MarketProjection.RUNNER_DESCRIPTION };
        private static readonly string _maxResults = "1000";
        private static readonly PriceProjection _oddsPriceProjection = new PriceProjection()
        {
            PriceData = new HashSet<PriceData>() {
                PriceData.EX_BEST_OFFERS
            }
        };

        internal Dictionary<string, object> AddAdditionalArguments(Dictionary<string, object> args, MarketFilter filter)
        {
            if (typeof(T) == typeof(EventResult))
            {
                args[FILTER] = filter;
            }
            if (typeof(T) == typeof(MarketCatalogue))
            {
                args[FILTER] = filter;
                args[MAX_RESULTS] = _maxResults;
                args[MARKET_PROJECTION] = _marketProjection;
            }
            if (typeof(T) == typeof(MarketBook))
            {
                args[MARKET_IDS] = filter.MarketIds;
                args[PRICE_PROJECTION] = _oddsPriceProjection;
            }
            return args;
        }
    }
}
