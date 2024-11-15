using System.Net.Http.Json;
using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;

namespace Elhori.Portfolio.Client.Services;

public class ProjectService(HttpClient httpClient) : IProjectService
{
    public async Task<string> CreateAsync(ProjectDto projectDto, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/Projects", projectDto, cancellationToken);

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<string> UpdateAsync(ProjectDto projectDto, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("api/Projects", projectDto, cancellationToken);

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<string> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"api/Projects/{id}", cancellationToken);

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<ProjectDto> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetFromJsonAsync<ProjectDto>($"api/Projects/{id}", cancellationToken);

        return response!;
    }

    public async Task<PaginatedResult<ProjectDto>> GetPaginatedAsync(
        string searchQuery, int currentPage = 1, int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetFromJsonAsync<PaginatedResult<ProjectDto>>(
            $"api/Projects/page/{currentPage}/size/{pageSize}?searchQuery={Uri.EscapeDataString(searchQuery)}",
            cancellationToken
        );

        return response!;
    }
}