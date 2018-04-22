using Newtonsoft.Json;
using System.Text;

namespace OddsFetchingEntities
{
    public class EventResult
        :Entity
    {
        [JsonProperty(PropertyName = "event")]
        public Event Event { get; set; }

        [JsonProperty(PropertyName = "marketCount")]
        public int MarketCount { get; set; }

        public override string ToString()
        {
            return new StringBuilder().AppendFormat("{0}", "EventResult")
                        .AppendFormat(" : {0}", Event)
                        .AppendFormat(" : MarketCount={0}", MarketCount)
                        .ToString();
        }

        public override string IdStr { get { return Event.IdStr; } }
        public override string Name { get { return Event.Name; } }
    }
}
