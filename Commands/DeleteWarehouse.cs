using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Problems.Warehouse.Commands.Core;
using Problems.Warehouse.Infrastructure.DbAccess;

namespace Problems.Warehouse.Commands
{
    public record DeleteWarehouse(int WarehouseId) : IRequest;

    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class DeleteWarehouseHandler : BaseDataAccessRequestHandler<DeleteWarehouse>
    {
        public DeleteWarehouseHandler(WarehouseDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task InnerHandle(DeleteWarehouse request, CancellationToken cancellationToken)
        {
            var warehouse = await DbContext.Warehouses.FindAsync(request.WarehouseId);
            //todo implement soft deletion or check references to article items
            DbContext.Warehouses.Remove(warehouse);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}