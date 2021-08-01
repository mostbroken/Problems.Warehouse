using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Problems.Warehouse.Commands.Core;
using Problems.Warehouse.Infrastructure.DbAccess;

namespace Problems.Warehouse.Commands
{
    public record UpdateWarehouse (int Id, string Address) : IRequest;

    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class UpdateWarehouseHandler : BaseDataAccessRequestHandler<UpdateWarehouse>
    {
        public UpdateWarehouseHandler(WarehouseDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task InnerHandle(UpdateWarehouse request, CancellationToken cancellationToken)
        {
            var warehouse = await DbContext.Warehouses.FindAsync(request.Id);
            warehouse.UpdateAddress(request.Address);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}