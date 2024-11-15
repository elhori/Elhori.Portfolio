using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;

namespace Elhori.Portfolio.Application.Contracts.Services;

public interface IInfoService
{
    Task<string> CreateAsync(InfoDto infoDto, CancellationToken cancellationToken = default);
    Task<string> UpdateAsync(InfoDto infoDto, CancellationToken cancellationToken = default);
    Task<string> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<InfoDto> GetAsync(CancellationToken cancellationToken = default);
}