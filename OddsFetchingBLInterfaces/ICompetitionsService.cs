using OddsFetchingEntities;
using System.Collections.Generic;

namespace OddsFetchingBLInterfaces
{
    public interface ICompetitionsService
    {
        ISet<string> FetchCompetitionIds(MarketFilter filter);
    }
}
