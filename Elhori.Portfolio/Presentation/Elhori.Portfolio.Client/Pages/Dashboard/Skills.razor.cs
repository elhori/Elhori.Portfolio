using System.ComponentModel.DataAnnotations;
using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Elhori.Portfolio.Client.Pages.Dashboard;

public partial class Skills
{
    [Inject] private ISkillService SkillService { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    private PaginatedResult<SkillDto> _skills = new();
    private SkillViewModel Model { get; set; } = new();

    private string FormTitle { get; set; } = "Add Skill";
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

        _skills = await SkillService.GetPaginatedAsync(SearchQuery, currentPage);

        await InvokeAsync(StateHasChanged);
    }

    private async Task SearchAsync()
    {
        await LoadDataAsync();
    }

    private void SelectSkill(SkillDto skill)
    {
        Model = new SkillViewModel(skill);

        FormTitle = "Edit Skill";
        FormButtonText = "Edit";
        IsEdit = true;
    }

    private async Task DeleteAsync(int id)
    {
        var response = await SkillService.DeleteAsync(id);

        await ShowToast(response);

        await LoadDataAsync();
    }

    private async Task SubmitAsync()
    {
        if (IsEdit)
        {
            var response = await SkillService.UpdateAsync(Model.ToDto());

            await ShowToast(response);
        }
        else
        {
            var response = await SkillService.CreateAsync(Model.ToDto());

            await ShowToast(response);
        }

        await LoadDataAsync();

        ResetForm();
    }

    private void ResetForm()
    {
        Model = new SkillViewModel();

        FormTitle = "Add Skill";
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

    public class SkillViewModel
    {
        public SkillViewModel() { }

        public SkillViewModel(SkillDto skill)
        {
            Id = skill.Id;
            Name = skill.Name;
            Icon = skill.Icon;
        }

        public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Icon { get; set; } = string.Empty;

        public SkillDto ToDto() => new(Id, Name, Icon);
    }
}