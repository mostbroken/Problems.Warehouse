using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Problems.Warehouse.Commands.Core;
using Problems.Warehouse.Infrastructure.DbAccess;

namespace Problems.Warehouse.Commands
{
    public record CreateWarehouse (string Address) : IRequest<int>;

    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class CreateWarehouseHandler : BaseDataAccessRequestHandler<CreateWarehouse, int>
    {
        public CreateWarehouseHandler(WarehouseDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task<int> InnerHandle(CreateWarehouse request, CancellationToken cancellationToken)
        {
            var warehouse = new Domain.Entities.Warehouse(request.Address);
            DbContext.Warehouses.Add(warehouse);
            await DbContext.SaveChangesAsync(cancellationToken);
            return warehouse.Id;
        }
    }
}