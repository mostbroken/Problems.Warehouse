using System;
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
    public record ArticleInfo(int Id, string Name, string? Description);
    
    public record GetArticles : IRequest<IReadOnlyCollection<ArticleInfo>>;

    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class GetArticlesHandler : BaseDataAccessRequestHandler<GetArticles, IReadOnlyCollection<ArticleInfo>>
    {
        public GetArticlesHandler(WarehouseDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task<IReadOnlyCollection<ArticleInfo>> InnerHandle(GetArticles request, CancellationToken cancellationToken)
        {
            return await DbContext.Articles
                .Select(a => new ArticleInfo(a.Id, a.Name, a.Description))
                .ToArrayAsync(cancellationToken: cancellationToken);
        }
    }
}