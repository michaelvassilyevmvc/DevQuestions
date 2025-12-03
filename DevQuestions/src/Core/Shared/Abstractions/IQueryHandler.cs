namespace Shared.Abstractions;

public interface IQuery;

public interface IQueryHandler<TResponse, in TQuery>
    where TQuery : IQuery
{
    Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken);
}