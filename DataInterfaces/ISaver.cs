using OddsFetchingEntities;

namespace DataInterfaces
{
    public interface ISaver<T>
        where T:Entity
    {
        void Save(T entity);
    }
}
