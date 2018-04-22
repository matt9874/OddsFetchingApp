using Newtonsoft.Json;
using System;

namespace OddsFetchingEntities
{
    public class OddsOffer: Entity
    {
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "size")]
        public decimal Size { get; set; }

        public DateTime OfferDateTime { get; set; }

        //public MarketBook MarketBook { get; set; }

        //public MarketCatalogue MarketCat { get; set; }

        public string MarketId { get; set; }
        public string SelectionId { get; set; }

        public OddsOfferTypes OfferType { get; set; }
        
        public OddsOffer()
        {

        }
    }
}
