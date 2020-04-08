using PetaPoco.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetaPoco.Repository
{
    public abstract class CrudRepositoryBase<T, TPrimaryKeyType> : ReadRepositoryBase<T, TPrimaryKeyType>, Abstractions.ICrudRepository<T, TPrimaryKeyType> where T : class
    {
        private readonly ICrudRepositoryServiceCollection _crudRepositoryServiceCollection;

        /// <summary>
        /// Default constructor which will use <see cref="Configuration.DefaultConfiguration"/> for databaseFactory and serviceCollection
        /// </summary>
        public CrudRepositoryBase()
        {
            _crudRepositoryServiceCollection = Configuration.DefaultConfiguration.CrudServiceCollection ?? new CrudRepositoryServiceCollection();
        }

        public CrudRepositoryBase(Abstractions.IDatabaseFactory databaseFactory, ICrudRepositoryServiceCollection crudRepositoryServiceCollection)
            : base(databaseFactory)
        {

            _crudRepositoryServiceCollection = crudRepositoryServiceCollection ?? Configuration.DefaultConfiguration.CrudServiceCollection ?? new CrudRepositoryServiceCollection();
        }

        #region ICrudRepository Implementation

        public virtual T Add(T entity)
        {
            foreach (var x in _crudRepositoryServiceCollection)
            {
                x.BeforeAdd(this, entity);
            }

            using (var db = this.GetDatabase())
            {
                db.Insert(entity);

            }

            foreach (var x in _crudRepositoryServiceCollection)
            {
                x.AfterAdd(this, entity);
            }

            return entity;
        }

        public virtual void Remove(TPrimaryKeyType entityId)
        {

            foreach (var x in _crudRepositoryServiceCollection)
            {
                x.BeforeRemove(this, entityId);
            }

            using (var db = this.GetDatabase())
            {
                db.Delete<T>(entityId);
            }

            foreach (var x in _crudRepositoryServiceCollection)
            {
                x.AfterRemove(this, entityId);
            }

        }

        public virtual T Update(T entity)
        {
            foreach (var x in _crudRepositoryServiceCollection)
            {
                x.BeforeUpdate(this, entity);
            }

            using (var db = this.GetDatabase())
            {
                db.Update(entity);
            }

            foreach (var x in _crudRepositoryServiceCollection)
            {
                x.AfterUpdate(this, entity);
            }

            return entity;
        }
        #endregion

    }
}

