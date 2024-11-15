using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;
using Microsoft.AspNetCore.Components;

namespace Elhori.Portfolio.Client.Pages;

public partial class Home
{
    [Inject] private IProjectService ProjectService { get; set; } = default!;
    [Inject] private ISkillService SkillService { get; set; } = default!;
    [Inject] private IInfoService InfoService { get; set; } = default!;

    private PaginatedResult<ProjectDto> _projects = new();
    private PaginatedResult<SkillDto> _skills = new();
    private InfoDto _info = new(0, string.Empty, string.Empty, string.Empty, string.Empty);

    private bool IsInitialized { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        IsInitialized = true;

        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        if (!IsInitialized)
            return;

        _projects = await ProjectService.GetPaginatedAsync(string.Empty);

        _skills = await SkillService.GetPaginatedAsync(string.Empty);

        _info = await InfoService.GetAsync();

        await InvokeAsync(StateHasChanged);
    }
}