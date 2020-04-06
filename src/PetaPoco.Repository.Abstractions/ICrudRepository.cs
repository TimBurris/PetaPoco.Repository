namespace PetaPoco.Repository.Abstractions
{
    public interface ICrudRepository<T, TPrimaryKeyType>
        : IReadRepository<T, TPrimaryKeyType>
    {
        T Add(T entity);
        T Update(T entity);
        void Remove(TPrimaryKeyType entityId);
    }
}
