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
    public record IncrementArticleQuantity(int WarehouseId, int ArticleId) : IRequest;

    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class IncrementArticleQuantityHandler : BaseDataAccessRequestHandler<IncrementArticleQuantity>
    {
        public IncrementArticleQuantityHandler(WarehouseDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task InnerHandle(IncrementArticleQuantity request, CancellationToken cancellationToken)
        {
            var warehouse = await DbContext.Warehouses
                .Include(w => w.Articles).ThenInclude(a => a.Article)
                .SingleOrDefaultAsync(w => w.Id == request.WarehouseId, cancellationToken: cancellationToken);
            warehouse.IncrementArticleQuantity(new ArticleId(request.ArticleId));
            
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}