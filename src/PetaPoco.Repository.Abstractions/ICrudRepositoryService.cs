namespace PetaPoco.Repository.Abstractions
{
    public interface ICrudRepositoryService
    {
        void BeforeUpdate<T, TPrimaryKeyType>(Abstractions.ICrudRepository<T, TPrimaryKeyType> repository, T entity);
        void AfterUpdate<T, TPrimaryKeyType>(Abstractions.ICrudRepository<T, TPrimaryKeyType> repository, T entity);

        void BeforeAdd<T, TPrimaryKeyType>(Abstractions.ICrudRepository<T, TPrimaryKeyType> repository, T entity);
        void AfterAdd<T, TPrimaryKeyType>(Abstractions.ICrudRepository<T, TPrimaryKeyType> repository, T entity);

        void BeforeRemove<T, TPrimaryKeyType>(Abstractions.ICrudRepository<T, TPrimaryKeyType> repository, TPrimaryKeyType entityId);
        void AfterRemove<T, TPrimaryKeyType>(Abstractions.ICrudRepository<T, TPrimaryKeyType> repository, TPrimaryKeyType entityId);
    }
}

