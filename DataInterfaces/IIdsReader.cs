using OddsFetchingEntities;
using System.Collections.Generic;

namespace DataInterfaces
{
    public interface IIdsReader<T>
        where T : Entity
    {
        ISet<string> ReadIds(MarketFilter filter);
    }
}
