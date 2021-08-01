using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Problems.Warehouse.Commands.Core;
using Problems.Warehouse.Infrastructure.DbAccess;

namespace Problems.Warehouse.Commands
{
    public record DeleteArticle(int ArticleId) : IRequest;

    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class DeleteArticleHandler : BaseDataAccessRequestHandler<DeleteArticle>
    {
        public DeleteArticleHandler(WarehouseDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task InnerHandle(DeleteArticle request, CancellationToken cancellationToken)
        {
            var article = await DbContext.Articles.FindAsync(request.ArticleId);
            //todo implement soft deletion or check references to article items
            DbContext.Articles.Remove(article);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}