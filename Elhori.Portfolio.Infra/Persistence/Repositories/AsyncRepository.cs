using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Elhori.Portfolio.Application.Contracts.Repositories;

namespace Elhori.Portfolio.Infra.Persistence.Repositories;

public class AsyncRepository<TDomain>(AppDbContext context) : IAsyncRepository<TDomain> where TDomain : class
{
    public async Task AddAsync(TDomain entity, CancellationToken cancellationToken = default)
    {
        await context.Set<TDomain>().AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<TDomain> entities, CancellationToken cancellationToken = default)
    {
        await context.Set<TDomain>().AddRangeAsync(entities, cancellationToken);
    }

    public async Task DeleteAsync(TDomain entity, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;

        context.Set<TDomain>().Remove(entity);
    }

    public async Task DeleteRangeAsync(IList<TDomain> entities, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;

        context.Set<TDomain>().RemoveRange(entities);
    }

    public async Task<TDomain> FindAsync(int id, bool includeRelated = false, CancellationToken cancellationToken = default)
    {
        return await context.Set<TDomain>().FindAsync(new object[] { id }, cancellationToken) ?? null!;
    }

    public async Task<TDomain> FindAsync(int id, Func<IQueryable<TDomain>, IQueryable<TDomain>> include = null!, CancellationToken cancellationToken = default)
    {
        IQueryable<TDomain> query = context.Set<TDomain>();

        if (include != null!)
        {
            query = include(query);
        }

        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id, cancellationToken) ?? null!;
    }

    public async Task<IEnumerable<TDomain>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<TDomain>().AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TDomain, TResult>> target, CancellationToken cancellationToken = default)
    {
        return await context.Set<TDomain>().AsNoTracking()
            .Select(target)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TDomain, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await context.Set<TDomain>().AsNoTracking()
            .AnyAsync(predicate, cancellationToken);
    }
}