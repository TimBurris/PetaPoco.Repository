using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDependencyInjection.Data.Repositories.Abstractions
{
    public interface ISalesOrderRepository : PetaPoco.Repository.Abstractions.ICrudRepository<Models.SalesOrder, int>
    {
        //all the normal crud stuff is defined in ICrudRepository, so we only need to define methods that are beyond the basic crud


        IEnumerable<Models.SalesOrder> FindByMinSalesAmount(decimal minSalesAmount);
    }
}
