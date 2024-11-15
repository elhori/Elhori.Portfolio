using Elhori.Portfolio.Domain.Dtos;
using FluentValidation;

namespace Elhori.Portfolio.Validators;

public class InfoDtoValidator : AbstractValidator<InfoDto>
{
    public InfoDtoValidator()
    {
        RuleFor(x => x.AboutMe)
            .NotEmpty().WithMessage("About Me section is required.")
            .MaximumLength(500).WithMessage("About Me section cannot exceed 500 characters.");

        RuleFor(x => x.GithubUrl)
            .NotEmpty().WithMessage("GitHub URL is required.")
            .MaximumLength(50).WithMessage("GitHub URL cannot exceed 50 characters.");

        RuleFor(x => x.LinkedInUrl)
            .NotEmpty().WithMessage("LinkedIn URL is required.")
            .MaximumLength(50).WithMessage("LinkedIn URL cannot exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(50).WithMessage("Email cannot exceed 50 characters.")
            .EmailAddress().WithMessage("A valid email address is required.");
    }
}