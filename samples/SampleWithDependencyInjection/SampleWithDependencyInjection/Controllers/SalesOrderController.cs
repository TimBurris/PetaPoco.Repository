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
    }
}
