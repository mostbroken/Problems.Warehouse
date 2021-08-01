using System.Collections.Generic;
using System.Linq;
using Problems.Warehouse.Api.WarehouseEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Problems.Warehouse.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly ILogger<WarehouseController> _logger;

        public WarehouseController(ILogger<WarehouseController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WarehouseDto> Get()
        {
            return Enumerable
                .Range(1, 3)
                .Select(x => new WarehouseDto ( x, $"address{x}"))
                .ToArray();
        }
    }
}