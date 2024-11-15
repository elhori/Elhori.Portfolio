using Elhori.Portfolio.Application.Contracts.Repositories;
using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Application.Resources;
using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;
using Elhori.Portfolio.Domain.Entities;

namespace Elhori.Portfolio.Application.Services;

public class InfoService(IUnitOfWork unitOfWork) : IInfoService
{
    public async Task<string> CreateAsync(InfoDto infoDto, CancellationToken cancellationToken = default)
    {
        if (await unitOfWork.Information.AnyAsync(i => !string.IsNullOrEmpty(i.AboutMe), cancellationToken: cancellationToken))
            return ResponseMessages.AlreadyExists;

        var info = new Info(infoDto);

        await unitOfWork.Information.AddAsync(info, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseMessages.Added;
    }

    public async Task<string> UpdateAsync(InfoDto infoDto, CancellationToken cancellationToken = default)
    {
        var info = await unitOfWork.Information.FindAsync(infoDto.Id, includeRelated: false, cancellationToken: cancellationToken);

        if (info == null!)
            return ResponseMessages.NotFound;

        info.Update(infoDto);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseMessages.Updated;
    }

    public async Task<string> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var info = await unitOfWork.Information.FindAsync(id, includeRelated: false, cancellationToken: cancellationToken);

        if (info == null!)
            return ResponseMessages.NotFound;

        await unitOfWork.Information.DeleteAsync(info, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseMessages.Deleted;
    }

    public async Task<InfoDto> GetAsync(CancellationToken cancellationToken = default)
    {
        var info = (await unitOfWork.Information.GetAllAsync(cancellationToken)).SingleOrDefault();

        if (info == null!)
            return new InfoDto(
                0,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty);

        return info.ToDto();
    }
}