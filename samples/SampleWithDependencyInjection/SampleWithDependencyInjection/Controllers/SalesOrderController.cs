using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleWithDependencyInjection.Data.Repositories.Abstractions;

namespace SampleWithDependencyInjection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesOrderController : ControllerBase
    {
        private readonly ISalesOrderRepository _salesOrderRepository;


        //our repos can just be injected wherever needed
        public SalesOrderController(Data.Repositories.Abstractions.ISalesOrderRepository salesOrderRepository)
        {
            _salesOrderRepository = salesOrderRepository;
        }



        [HttpGet]
        public IEnumerable<Data.Models.SalesOrder> Get()
        {
            return _salesOrderRepository.GetAll();
        }

        [HttpGet("CreateAndGet")]
        public IEnumerable<Data.Models.SalesOrder> CreateAndGet()
        {

            var newRec = new Data.Models.SalesOrder()
            {
                SoldBy = "Jenny, Jenny",
                Amount = 86753.09m,
            };
            
            _salesOrderRepository.Add(newRec);

            return _salesOrderRepository.GetAll();
        }
    }
}
