using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Problems.Warehouse.Commands.Core;
using Problems.Warehouse.Infrastructure.DbAccess;

namespace Problems.Warehouse.Commands
{
    public record WarehouseItem(int Id, string Address);

    public record GetWarehouses : IRequest<IReadOnlyCollection<WarehouseItem>>;
    
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class GetWarehousesHandler : BaseDataAccessRequestHandler<GetWarehouses, IReadOnlyCollection<WarehouseItem>>
    {
        public GetWarehousesHandler(WarehouseDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task<IReadOnlyCollection<WarehouseItem>> InnerHandle(GetWarehouses request, CancellationToken cancellationToken)
        {
            return await DbContext.Warehouses
                .Select(w => new WarehouseItem(w.Id, w.Address))
                .ToArrayAsync(cancellationToken);
        }
    }
}