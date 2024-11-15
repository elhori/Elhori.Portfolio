using System.Net.Http.Json;
using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;

namespace Elhori.Portfolio.Client.Services;

public class SkillService(HttpClient httpClient) : ISkillService
{
    public async Task<string> CreateAsync(SkillDto skillDto, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/Skills", skillDto, cancellationToken);

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<string> UpdateAsync(SkillDto skillDto, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("api/Skills", skillDto, cancellationToken);

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<string> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"api/Skills/{id}", cancellationToken);

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<SkillDto> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetFromJsonAsync<SkillDto>($"api/Skills/{id}", cancellationToken);

        return response!;
    }

    public async Task<PaginatedResult<SkillDto>> GetPaginatedAsync(
        string searchQuery, int currentPage = 1, int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetFromJsonAsync<PaginatedResult<SkillDto>>(
            $"api/Skills/page/{currentPage}/size/{pageSize}?searchQuery={Uri.EscapeDataString(searchQuery)}",
            cancellationToken
        );

        return response!;
    }
}
