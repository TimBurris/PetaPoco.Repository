using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleWithDefaultConfiguration.Data.Repositories.Abstractions;

namespace SampleWithDefaultConfiguration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesOrderController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Data.Models.SalesOrder> Get()
        {
            var repo = new Data.Repositories.SalesOrderRepository();
            return repo.GetAll();
        }
    }
}
