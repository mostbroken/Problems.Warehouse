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
    public class ArticleController : ControllerBase
    {
        private readonly ILogger<WarehouseController> _logger;
        private readonly IMediator _mediator;

        public ArticleController(ILogger<WarehouseController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IEnumerable<ArticleInfo>> Get()
        {
            return await _mediator.Send(new GetArticles());
        }
        
        [HttpPost]
        public async Task Create(string name, string description)
        {
            await _mediator.Send(new CreateArticle(name, description));
        }        
        
        [HttpPut]
        public async Task Update(int id, string newName, string newDescription)
        {
            await _mediator.Send(new UpdateArticle(id, newName, newDescription));
        }       
        
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _mediator.Send(new DeleteArticle(id));
        }
    }
}