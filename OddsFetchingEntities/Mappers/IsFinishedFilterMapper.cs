using System;
using System.Linq.Expressions;

namespace OddsFetchingEntities.Mappers
{
    public class IsFinishedFilterMapper : IMapper<MarketFilter, Expression<Func<MarketCatalogue, bool>>>
    {
        public Expression<Func<MarketCatalogue, bool>> Map(MarketFilter marketFilter)
        {
            if (marketFilter.IsFinished == true || marketFilter.IsFinished == false)
            {
                return o => o.IsFinished == marketFilter.IsFinished;
            }
            return o => true;
        }
    }
}
