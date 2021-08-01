using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Problems.Warehouse.Commands;

namespace Problems.Warehouse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PingController : ControllerBase
    {
        private readonly ILogger<PingController> _logger;

        private readonly IMediator _mediator;

        public PingController(IMediator mediator, ILogger<PingController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]

        public async Task<string> Get()
        {
            return await _mediator.Send(new Ping());
        }
    }
}