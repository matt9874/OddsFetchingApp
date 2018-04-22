using Newtonsoft.Json;
using System.Collections.Generic;

namespace OddsFetchingEntities
{
    public class ExchangeInfo
    {
        [JsonProperty(PropertyName = "availableToBack")]
        public List<OddsOffer> AvailableToBack { get; set; }

        [JsonProperty(PropertyName = "availableToLay")]
        public List<OddsOffer> AvailableToLay { get; set; }
    }
}
