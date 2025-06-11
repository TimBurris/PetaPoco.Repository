using System.Collections.Generic;

namespace PetaPoco.Repository.Abstractions
{
    public interface IReadRepository<T, TPrimaryKeyType> : IRepository
    {
        T FindById(TPrimaryKeyType entityId);

        /// <param name="batchSize">MSSQL only allows 2,100 params, so the default batch size is 2,000.  The ids will be batched up and the sql statement will be executed for each batch, returning a list of all the batched records combined</param>
        IEnumerable<T> FindAllByIds(IEnumerable<TPrimaryKeyType> entityIds, int batchSize = 2000);
        IEnumerable<T> GetAll();
    }
}
