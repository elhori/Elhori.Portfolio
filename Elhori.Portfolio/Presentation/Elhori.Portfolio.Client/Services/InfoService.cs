using System.Net.Http.Json;
using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Domain.Dtos;

namespace Elhori.Portfolio.Client.Services;

public class InfoService(HttpClient httpClient) : IInfoService
{
    public async Task<string> CreateAsync(InfoDto infoDto, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/Information", infoDto, cancellationToken);

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<string> UpdateAsync(InfoDto infoDto, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("api/Information", infoDto, cancellationToken);

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<string> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"api/Information/{id}", cancellationToken);

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<InfoDto> GetAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetFromJsonAsync<InfoDto>($"api/Information", cancellationToken);

        return response!;
    }
}