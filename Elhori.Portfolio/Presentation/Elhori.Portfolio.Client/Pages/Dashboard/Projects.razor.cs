using System.ComponentModel.DataAnnotations;
using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Elhori.Portfolio.Client.Pages.Dashboard;

public partial class Projects
{
    [Inject] private IProjectService ProjectService { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    private PaginatedResult<ProjectDto> _projects = new();
    private ProjectViewModel Model { get; set; } = new();

    private string FormTitle { get; set; } = "Add Project";
    private string FormButtonText { get; set; } = "Add";
    private bool IsEdit { get; set; } = false;
    private bool IsInitialized { get; set; } = false;
    private string SearchQuery { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        IsInitialized = true;

        await LoadDataAsync();
    }

    private async Task LoadDataAsync(int currentPage = 1)
    {
        if (!IsInitialized)
            return;

        _projects = await ProjectService.GetPaginatedAsync(SearchQuery, currentPage);

        await InvokeAsync(StateHasChanged);
    }

    private async Task SearchAsync()
    {
        await LoadDataAsync();
    }

    private void SelectProject(ProjectDto project)
    {
        Model = new ProjectViewModel(project);

        FormTitle = "Edit Project";
        FormButtonText = "Edit";
        IsEdit = true;
    }

    private async Task DeleteAsync(int id)
    {
        var response = await ProjectService.DeleteAsync(id);

        await ShowToast(response);

        await LoadDataAsync();
    }

    private async Task SubmitAsync()
    {
        if (IsEdit)
        {
            var response = await ProjectService.UpdateAsync(Model.ToDto());

            await ShowToast(response);
        }
        else
        {
            var response = await ProjectService.CreateAsync(Model.ToDto());

            await ShowToast(response);
        }

        await LoadDataAsync();

        ResetForm();
    }

    private void ResetForm()
    {
        Model = new ProjectViewModel();

        FormTitle = "Add Project";
        FormButtonText = "Add";
        IsEdit = false;
    }

    private async Task ShowToast(string response)
    {
        var bgClass = response switch
        {
            "تم التعديل بنجاح" => "bg-success",
            "تم الحذف بنجاح" => "bg-success",
            "تمت الإضافة بنجاح" => "bg-success",
            "العنصر موجود بالفعل" => "bg-warning",
            _ => "bg-danger"
        };
        await JsRuntime.InvokeVoidAsync("ShowToast", response, bgClass);
    }

    public class ProjectViewModel
    {
        public ProjectViewModel() { }

        public ProjectViewModel(ProjectDto projectDto)
        {
            Id = projectDto.Id;
            Name = projectDto.Name;
            Description = projectDto.Description;
        }

        public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Description { get; set; } = string.Empty;

        public ProjectDto ToDto() => new(Id, Name, Description);
    }
}