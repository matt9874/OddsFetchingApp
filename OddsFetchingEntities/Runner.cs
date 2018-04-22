using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace OddsFetchingEntities
{
    public class Runner: Entity
    {
        //Possibly change to enumeration

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        public OddsOffer BestBackOffer { get
            {
                var orderedOffers = ExchangePricesAndOdds?.AvailableToBack.OrderByDescending(o => o.Price).ToList();
                OddsOffer offer = new OddsOffer();

                if (orderedOffers!=null && orderedOffers.Any())
                {
                    offer = orderedOffers[0];
                }
                SetOddsOfferIds(offer);
                offer.OfferType = OddsOfferTypes.Back;
                return offer;
            }
        }
        public OddsOffer BestLayOffer
        {
            get
            {
                var orderedOffers = ExchangePricesAndOdds?.AvailableToLay.OrderBy(o => o.Price).ToList();
                OddsOffer offer = new OddsOffer();

                if (orderedOffers != null && orderedOffers.Any())
                {
                    offer = orderedOffers[0];
                }
                SetOddsOfferIds(offer);
                offer.OfferType = OddsOfferTypes.Lay;
                return offer;
            }
        }
        public decimal ExchangeBackPrice {
            get { return BestBackOffer.Price; }
        }
        public decimal ExchangeBackSize
        {
            get { return BestBackOffer.Size; }
        }
        public decimal ExchangeLayPrice {
            get { return BestLayOffer.Price; }
        }
        public decimal ExchangeLaySize {
            get { return BestLayOffer.Size; }
        }

        [JsonProperty(PropertyName = "selectionId")]
        public int SelectionId { get; set; }

        [JsonProperty(PropertyName = "ex")]
        public ExchangeInfo ExchangePricesAndOdds { get; set; }
        
        public string MarketId { get; set; }

        private void SetOddsOfferIds(OddsOffer offer)
        {
            offer.MarketId = MarketId;
            offer.SelectionId = SelectionId.ToString();
        }
    }
}