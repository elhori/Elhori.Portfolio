using Elhori.Portfolio.Domain.Dtos;

namespace Elhori.Portfolio.Domain.Entities;

public class Project
{
    private Project() { }

    public Project(ProjectDto dto)
    {
        Name = dto.Name;
        Description = dto.Description;
    }

    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public void Update(ProjectDto dto)
    {
        Name = dto.Name;
        Description = dto.Description;
    }

    public ProjectDto ToDto()
    {
        return new ProjectDto(Id, Name, Description);
    }
}