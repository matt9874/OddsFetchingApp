using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OddsFetchingEntities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MarketProjection
    {
        COMPETITION, EVENT, EVENT_TYPE, MARKET_DESCRIPTION, RUNNER_DESCRIPTION, RUNNER_METADATA
    }
}
