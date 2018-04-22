using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OddsFetchingEntities
{
    public class Event: Entity
    {
        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty(PropertyName = "timezone")]
        public string Timezone { get; set; }

        [JsonProperty(PropertyName = "venue")]
        public string Venue { get; set; }

        [JsonProperty(PropertyName = "openDate")]
        public DateTime? OpenDate { get; set; }

        public IEnumerable<MarketType> MarketTypes { get; set; }

        [BsonIgnore]
        public IEnumerable<MarketCatalogue> Markets { get; set; }

        public string CompetitionId { get; set; }
    }
}
