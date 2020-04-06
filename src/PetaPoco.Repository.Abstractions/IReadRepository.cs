using System;
using System.Collections.Generic;
using System.Text;

namespace PetaPoco.Repository.Abstractions
{
    public interface IReadRepository<T, TPrimaryKeyType>
    {
        T FindById(TPrimaryKeyType entityId);
        List<T> FindAllByIds(IEnumerable<TPrimaryKeyType> entityIds);
        List<T> GetAll();
    }
}
