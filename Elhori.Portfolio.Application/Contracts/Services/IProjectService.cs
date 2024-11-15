using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;

namespace Elhori.Portfolio.Application.Contracts.Services;

public interface IProjectService
{
    Task<string> CreateAsync(ProjectDto projectDto, CancellationToken cancellationToken = default);
    Task<string> UpdateAsync(ProjectDto projectDto, CancellationToken cancellationToken = default);
    Task<string> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<ProjectDto> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<PaginatedResult<ProjectDto>> GetPaginatedAsync(string searchQuery, int currentPage = 1, int pageSize = 10,
        CancellationToken cancellationToken = default);
}