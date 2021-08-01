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
    public record ArticleItemInfo(int Id, string Name, string? Description, int Quantity);
    
    public record WarehouseDetails(int Id, string Address, IReadOnlyCollection<ArticleItemInfo> Articles);

    public record GetWarehouseDetails(int Id) : IRequest<WarehouseDetails>;
    
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class GetWarehouseDetailsHandler : BaseDataAccessRequestHandler<GetWarehouseDetails, WarehouseDetails>
    {
        public GetWarehouseDetailsHandler(WarehouseDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task<WarehouseDetails> InnerHandle(GetWarehouseDetails request, CancellationToken cancellationToken)
        {
            var warehouse = await DbContext.Warehouses
                .Include(w => w.Articles).ThenInclude(a => a.Article)
                .SingleOrDefaultAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);

            return new WarehouseDetails(
                warehouse.Id,
                warehouse.Address,
                warehouse.Articles.Select(a =>
                    new ArticleItemInfo(
                        a.ArticleId, 
                        a.Article.Name, 
                        a.Article.Description, 
                        a.Quantity)).ToList().AsReadOnly()
                );
        }
    }
}