using Elhori.Portfolio.Domain.Dtos;
using FluentValidation;

namespace Elhori.Portfolio.Validators;

public class SkillDtoValidator : AbstractValidator<SkillDto>
{
    public SkillDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Skill name is required.")
            .MaximumLength(50).WithMessage("Skill name cannot exceed 50 characters.");

        RuleFor(x => x.Icon)
            .NotEmpty().WithMessage("Icon is required.")
            .MaximumLength(50).WithMessage("Icon cannot exceed 50 characters.");
    }
}