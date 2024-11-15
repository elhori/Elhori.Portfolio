using Elhori.Portfolio.Application.Contracts.Repositories;
using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;
using Elhori.Portfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elhori.Portfolio.Infra.Persistence.Repositories;

public class SkillRepository(AppDbContext context) : AsyncRepository<Skill>(context), ISkillRepository
{
    private readonly AppDbContext _context = context;

    public async Task<PaginatedResult<SkillDto>> GetPaginatedAsync(string searchQuery, int currentPage = 1, int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var data = _context.Skills.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            data = data.Where(e => e.Name.Contains(searchQuery));
        }

        var total = await data.CountAsync(cancellationToken);

        var skip = (currentPage - 1) * pageSize;

        if (!await data.Skip(skip).AnyAsync(cancellationToken: cancellationToken))
            return new PaginatedResult<SkillDto>
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalResults = total,
                Results = []
            };

        var results = await data
            .Skip(skip)
            .Take(pageSize)
            .Select(i => i.ToDto())
            .ToListAsync(cancellationToken);

        return new PaginatedResult<SkillDto>
        {
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalResults = total,
            Results = results
        };
    }
}