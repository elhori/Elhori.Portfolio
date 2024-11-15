using Elhori.Portfolio.Application.Contracts.Repositories;
using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Application.Resources;
using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;
using Elhori.Portfolio.Domain.Entities;

namespace Elhori.Portfolio.Application.Services;

public class ProjectService(IUnitOfWork unitOfWork) : IProjectService
{
    public async Task<string> CreateAsync(ProjectDto projectDto, CancellationToken cancellationToken = default)
    {
        if (await unitOfWork.Projects.AnyAsync(i => i.Name == projectDto.Name, cancellationToken: cancellationToken))
            return ResponseMessages.AlreadyExists;

        var project = new Project(projectDto);

        await unitOfWork.Projects.AddAsync(project, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseMessages.Added;
    }

    public async Task<string> UpdateAsync(ProjectDto projectDto, CancellationToken cancellationToken = default)
    {
        var project = await unitOfWork.Projects.FindAsync(projectDto.Id, includeRelated: false, cancellationToken: cancellationToken);

        if (project == null!)
            return ResponseMessages.NotFound;

        project.Update(projectDto);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseMessages.Updated;
    }

    public async Task<string> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var project = await unitOfWork.Projects.FindAsync(id, includeRelated: false, cancellationToken: cancellationToken);

        if (project == null!)
            return ResponseMessages.NotFound;

        await unitOfWork.Projects.DeleteAsync(project, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseMessages.Deleted;
    }

    public async Task<ProjectDto> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var project = await unitOfWork.Projects.FindAsync(id, includeRelated: false, cancellationToken: cancellationToken);

        if (project == null!)
            return new ProjectDto(
                0,
                string.Empty,
                string.Empty);

        return project.ToDto();
    }

    public async Task<PaginatedResult<ProjectDto>> GetPaginatedAsync(string searchQuery, int currentPage = 1, int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        return await unitOfWork.Projects.GetPaginatedAsync(searchQuery, currentPage, pageSize, cancellationToken);
    }
}