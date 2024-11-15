using System.ComponentModel.DataAnnotations;
using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;
using Elhori.Portfolio.Domain.Entities;

namespace Elhori.Portfolio.Application.Contracts.Repositories;

public interface ISkillRepository : IAsyncRepository<Skill>
{
    Task<PaginatedResult<SkillDto>> GetPaginatedAsync(
        string searchQuery,
        [Range(1, int.MaxValue)] int currentPage = 1,
        [Range(1, int.MaxValue)] int pageSize = 10,
        CancellationToken cancellationToken = default);
}