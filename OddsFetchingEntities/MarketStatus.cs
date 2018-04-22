using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OddsFetchingEntities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MarketStatus
    {
        INACTIVE, OPEN, SUSPENDED, CLOSED
    }
}
