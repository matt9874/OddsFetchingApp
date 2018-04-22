using System.Collections.Generic;

namespace OddsFetchingBLInterfaces
{
    public interface IMarketBooksService
    {
        void FetchAndStoreOdds(IEnumerable<string> marketIds);
        ISet<string> FetchClosedMarketIds(ISet<string> ids);
    }
}
