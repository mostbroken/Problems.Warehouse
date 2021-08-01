using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Problems.Warehouse.Commands.Core;
using Problems.Warehouse.Infrastructure.DbAccess;

namespace Problems.Warehouse.Commands
{
    public record UpdateArticle (int Id, string Name, string Description) : IRequest;

    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class UpdateArticleHandler : BaseDataAccessRequestHandler<UpdateArticle>
    {
        public UpdateArticleHandler(WarehouseDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task InnerHandle(UpdateArticle request, CancellationToken cancellationToken)
        {
            var article = await DbContext.Articles.FindAsync(request.Id);
            article.UpdateName(request.Name);
            article.UpdateDescription(request.Description);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}