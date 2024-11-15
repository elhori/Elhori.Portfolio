namespace Elhori.Portfolio.Domain.Dtos;

public record InfoDto(
    int Id,
    string AboutMe,
    string GithubUrl,
    string LinkedInUrl,
    string Email);