using PetaPoco.Repository.Abstractions;
using SampleWithDefaultConfiguration.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDefaultConfiguration.Data.Repositories
{
    public class SalesOrderRepository : PetaPoco.Repository.CrudRepositoryBase<Models.SalesOrder, int>, Abstractions.ISalesOrderRepository
    {
        public SalesOrderRepository()
        {
        }

        public IEnumerable<SalesOrder> FindByMinSalesAmount(decimal minSalesAmount)
        {
            return this.Query($"WHERE {nameof(EntityType.Amount)} >= {minSalesAmount}")
                .ToList();
        }
    }
}
