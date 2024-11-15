using Elhori.Portfolio.Domain.Dtos;
using FluentValidation;

namespace Elhori.Portfolio.Validators;

public class ProjectDtoValidator : AbstractValidator<ProjectDto>
{
    public ProjectDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(50).WithMessage("Project name cannot exceed 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}