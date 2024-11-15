using Elhori.Portfolio.Domain.Dtos;

namespace Elhori.Portfolio.Domain.Entities;

public class Info
{
    private Info() { }

    public Info(InfoDto dto)
    {
        AboutMe = dto.AboutMe;
        GithubUrl = dto.GithubUrl;
        LinkedInUrl = dto.LinkedInUrl;
        Email = dto.Email;
    }

    public int Id { get; private set; }

    public string AboutMe { get; private set; } = string.Empty;

    public string GithubUrl { get; private set; } = string.Empty;

    public string LinkedInUrl { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public void Update(InfoDto dto)
    {
        AboutMe = dto.AboutMe;
        GithubUrl = dto.GithubUrl;
        LinkedInUrl = dto.LinkedInUrl;
        Email = dto.Email;
    }

    public InfoDto ToDto()
    {
        return new InfoDto(Id, AboutMe, GithubUrl, LinkedInUrl, Email);
    }
}