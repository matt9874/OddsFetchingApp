using Newtonsoft.Json;
using System.Collections.Generic;
using MongoDB.Bson;

namespace OddsFetchingEntities
{
    public class MarketCatalogue: Entity
    {
        public MarketCatalogue()
        {
            IsFinished = false;
        }

        public ObjectId _id { get; set; }

        [JsonProperty(PropertyName = "marketId")]
        public string MarketId { get; set; }

        [JsonProperty(PropertyName = "marketName")]
        public string MarketName { get; set; }

        [JsonProperty(PropertyName = "isMarketDataDelayed")]
        public bool IsMarketDataDelayed { get; set; }

        [JsonProperty(PropertyName = "eventType")]
        public EventType EventType { get; set; }

        [JsonProperty(PropertyName = "runners")]
        public List<RunnerDescription> Runners { get; set; }

        public override string IdStr
        {
            get
            {
                return MarketId;
            }
        }

        //public string CompetitionId { get; set; }
        public string EventId { get; set; }

        public bool IsFinished { get; set; }
    }
}
