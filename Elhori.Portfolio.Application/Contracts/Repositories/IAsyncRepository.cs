using System.Linq.Expressions;

namespace Elhori.Portfolio.Application.Contracts.Repositories;

public interface IAsyncRepository<TDomain> where TDomain : class
{
    Task AddAsync(TDomain entity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<TDomain> entities, CancellationToken cancellationToken = default);

    Task DeleteAsync(TDomain entity, CancellationToken cancellationToken = default);

    Task DeleteRangeAsync(IList<TDomain> entities, CancellationToken cancellationToken = default);

    Task<TDomain> FindAsync(int id, bool includeRelated = false, CancellationToken cancellationToken = default);

    Task<TDomain> FindAsync(int id, Func<IQueryable<TDomain>, IQueryable<TDomain>> include = null!,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TDomain>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TDomain, TResult>> target, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<TDomain, bool>> predicate, CancellationToken cancellationToken = default);
}