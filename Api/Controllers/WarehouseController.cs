using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Problems.Warehouse.Commands;

namespace Problems.Warehouse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly ILogger<WarehouseController> _logger;
        private readonly IMediator _mediator;

        public WarehouseController(ILogger<WarehouseController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IEnumerable<WarehouseItem>> Get()
        {
            return await _mediator.Send(new GetWarehouses());
        }
        
        [HttpGet("Details")]
        public async Task<WarehouseDetails> Get(int id)
        {
            return await _mediator.Send(new GetWarehouseDetails(id));
        }
        
        [HttpPost]
        public async Task Create(string address)
        {
            await _mediator.Send(new CreateWarehouse(address));
        }        
        
        [HttpPut]
        public async Task Update(int id, string newAddress)
        {
            await _mediator.Send(new UpdateWarehouse(id, newAddress));
        }
        
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _mediator.Send(new DeleteWarehouse(id));
        }

                
        [HttpPost("IncrementArticle")]
        public async Task IncrementArticleQuantity(int warehouseId, int articleId)
        {
            await _mediator.Send(new IncrementArticleQuantity(warehouseId, articleId));
        }
        
        [HttpPost("DecrementArticle")]
        public async Task DecrementArticleQuantity(int warehouseId, int articleId)
        {
            await _mediator.Send(new DecrementArticleQuantity(warehouseId, articleId));
        }
    }
}