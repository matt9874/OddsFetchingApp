using OddsFetchingEntities;
using System;
using System.Collections.Generic;

namespace ApiClients.JsonClientHelpers
{
    internal class UrlSuffixGetter<T>
    {
        private static readonly string _listEventsMethod = "SportsAPING/v1.0/listEvents";
        private static readonly string _listMarketCatalogueMethod = "SportsAPING/v1.0/listMarketCatalogue";
        private static readonly string _listMarketBookMethod = "SportsAPING/v1.0/listMarketBook";

        private Dictionary<Type, string> _methodStringMappings = new Dictionary<Type, string>()
        {
            {typeof(EventResult), _listEventsMethod },
            {typeof(MarketCatalogue), _listMarketCatalogueMethod },
            {typeof(MarketBook), _listMarketBookMethod }
        };

        internal string GetUrlSuffix()
        {
            if (_methodStringMappings.ContainsKey(typeof(T)))
            {
                return _methodStringMappings[typeof(T)];
            }
            return "";
        }
    }
}
