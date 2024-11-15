using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elhori.Portfolio.Application.Contracts.Repositories;
using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Application.Resources;
using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;
using Elhori.Portfolio.Domain.Entities;

namespace Elhori.Portfolio.Application.Services;

public class SkillService(IUnitOfWork unitOfWork) : ISkillService
{
    public async Task<string> CreateAsync(SkillDto skillDto, CancellationToken cancellationToken = default)
    {
        if (await unitOfWork.Skills.AnyAsync(i => i.Name == skillDto.Name, cancellationToken: cancellationToken))
            return ResponseMessages.AlreadyExists;

        var skill = new Skill(skillDto);

        await unitOfWork.Skills.AddAsync(skill, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseMessages.Added;
    }

    public async Task<string> UpdateAsync(SkillDto skillDto, CancellationToken cancellationToken = default)
    {
        var skill = await unitOfWork.Skills.FindAsync(skillDto.Id, includeRelated: false, cancellationToken: cancellationToken);

        if (skill == null!)
            return ResponseMessages.NotFound;

        skill.Update(skillDto);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseMessages.Updated;
    }

    public async Task<string> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var skill = await unitOfWork.Skills.FindAsync(id, includeRelated: false, cancellationToken: cancellationToken);

        if (skill == null!)
            return ResponseMessages.NotFound;

        await unitOfWork.Skills.DeleteAsync(skill, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseMessages.Deleted;
    }

    public async Task<SkillDto> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var skill = await unitOfWork.Skills.FindAsync(id, includeRelated: false, cancellationToken: cancellationToken);

        if (skill == null!)
            return new SkillDto(0, string.Empty, string.Empty);

        return skill.ToDto();
    }

    public async Task<PaginatedResult<SkillDto>> GetPaginatedAsync(string searchQuery, int currentPage = 1, int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        return await unitOfWork.Skills.GetPaginatedAsync(searchQuery, currentPage, pageSize, cancellationToken);
    }
}