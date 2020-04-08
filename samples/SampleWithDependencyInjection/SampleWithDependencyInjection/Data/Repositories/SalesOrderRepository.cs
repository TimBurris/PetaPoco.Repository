using PetaPoco.Repository.Abstractions;
using SampleWithDependencyInjection.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDependencyInjection.Data.Repositories
{
    public class SalesOrderRepository : PetaPoco.Repository.CrudRepositoryBase<Models.SalesOrder, int>, Abstractions.ISalesOrderRepository
    {
        public SalesOrderRepository(IDatabaseFactory databaseFactory, ICrudRepositoryServiceCollection crudRepositoryServiceCollection)
            : base(databaseFactory, crudRepositoryServiceCollection)
        {
        }

        public IEnumerable<SalesOrder> FindByMinSalesAmount(decimal minSalesAmount)
        {
            return this.Query($"WHERE {nameof(EntityType.Amount)} >= {minSalesAmount}")
                .ToList();
        }
    }
}
