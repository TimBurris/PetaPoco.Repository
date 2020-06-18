using System;
using System.Collections.Generic;
using System.Text;

namespace PetaPoco.Repository.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// if you need comments on what 'Commit' in a 'Unit of Work' does, you might consider another career.
        /// </summary>
        void Commit();

        /// <summary>
        /// the database which repositories must use to be part of the unit of work
        /// </summary>
        IDatabase Db { get; }

        /// <summary>
        /// Manually enlists a repository into this unit of work.
        /// </summary>
        /// <param name="repository"></param>
        void Enlist(IRepository repository);
    }
}
