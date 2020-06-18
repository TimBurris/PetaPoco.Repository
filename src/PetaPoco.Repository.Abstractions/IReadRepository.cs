using System;
using System.Collections.Generic;
using System.Text;

namespace PetaPoco.Repository.Abstractions
{
    public interface IReadRepository<T, TPrimaryKeyType> : IRepository
    {
        T FindById(TPrimaryKeyType entityId);
        IEnumerable<T> FindAllByIds(IEnumerable<TPrimaryKeyType> entityIds);
        IEnumerable<T> GetAll();
    }
}
