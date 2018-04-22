using OddsFetchingEntities;
using System.Collections.Generic;

namespace DataInterfaces
{
    public interface IReader<T>
        where T : Entity
    {
        IEnumerable<T> Read(MarketFilter filter);
    }
}
