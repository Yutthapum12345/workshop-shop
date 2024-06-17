using MediatR;

namespace BuildingBlocks.CQRS;

 public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery, Unit> where TQuery : IQuery<Unit>
    {
        // No additional members needed for this interface
    }

    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse> where TResponse : notnull
    {
        // No additional members needed for this interface
    }

   