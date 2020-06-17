using System;
using System.Collections.Generic;
using System.Text;

namespace PetaPoco.Repository.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        IDatabase Db { get; }
    }
}
