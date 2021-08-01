using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Problems.Warehouse.Commands
{
    public record Ping : IRequest<string>;
    
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public class PingHandler : IRequestHandler<Ping, string>
    {
        public Task<string> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task<string>.FromResult("Pong");
        }
    }
}
