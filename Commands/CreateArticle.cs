using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Problems.Warehouse.Commands.Core;
using Problems.Warehouse.Domain.Entities;
using Problems.Warehouse.Infrastructure.DbAccess;

namespace Problems.Warehouse.Commands
{
    public record CreateArticle(string Name, string Description) : IRequest<int>;

    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class CreateArticleHandler : BaseDataAccessRequestHandler<CreateArticle, int>
    {
        public CreateArticleHandler(WarehouseDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task<int> InnerHandle(CreateArticle request, CancellationToken cancellationToken)
        {
            var article = new Article(request.Name, request.Description);
            DbContext.Articles.Add(article);
            
            await DbContext.SaveChangesAsync(cancellationToken);
            return article.Id;
        }
    }
}