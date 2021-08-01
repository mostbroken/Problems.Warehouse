using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Problems.Warehouse.Infrastructure.DbAccess;

namespace Problems.Warehouse.Commands.Core
{
    public abstract class BaseDataAccessRequestHandler<TRequest> : IRequestHandler<TRequest>
        where TRequest : IRequest
    {
        protected readonly WarehouseDbContext DbContext;

        protected BaseDataAccessRequestHandler(WarehouseDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
        {
            await InnerHandle(request, cancellationToken);
            return Unit.Value;
        }

        protected abstract Task InnerHandle(TRequest request, CancellationToken cancellationToken);
    }


    public abstract class BaseDataAccessRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly WarehouseDbContext DbContext;

        protected BaseDataAccessRequestHandler(WarehouseDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
            => InnerHandle(request, cancellationToken);

        protected abstract Task<TResponse> InnerHandle(TRequest request, CancellationToken cancellationToken);
    }
}