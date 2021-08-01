using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Problems.Warehouse.Commands.Core;
using Problems.Warehouse.Domain.Entities;
using Problems.Warehouse.Infrastructure.DbAccess;

namespace Problems.Warehouse.Commands
{
    public record DecrementArticleQuantity(int WarehouseId, int ArticleId) : IRequest;

    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class DecrementArticleQuantityHandler : BaseDataAccessRequestHandler<DecrementArticleQuantity>
    {
        public DecrementArticleQuantityHandler(WarehouseDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task InnerHandle(DecrementArticleQuantity request, CancellationToken cancellationToken)
        {
            var warehouse = await DbContext.Warehouses
                .Include(w => w.Articles).ThenInclude(a => a.Article)
                .SingleOrDefaultAsync(w => w.Id == request.WarehouseId, cancellationToken: cancellationToken);
            
            warehouse.DecrementArticleQuantity(new ArticleId(request.ArticleId));
            
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}