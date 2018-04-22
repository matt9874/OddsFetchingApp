using System.Collections.Generic;

namespace DataInterfaces
{
    public interface IUpdater<T>
    {
        void UpdateMany(ISet<string> ids, string propertyName, string newValue);
    }
}
