using Elhori.Portfolio.Application.Contracts.Repositories;
using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;

namespace Elhori.Portfolio.Application.Contracts.Services;

public interface ISkillService
{
    Task<string> CreateAsync(SkillDto skillDto, CancellationToken cancellationToken = default);
    Task<string> UpdateAsync(SkillDto skillDto, CancellationToken cancellationToken = default);
    Task<string> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<SkillDto> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<PaginatedResult<SkillDto>> GetPaginatedAsync(string searchQuery, int currentPage = 1, int pageSize = 10,
        CancellationToken cancellationToken = default);
}