using System.ComponentModel.DataAnnotations;
using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Domain.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Elhori.Portfolio.Client.Pages.Dashboard;

public partial class Info
{
    [Inject] private IInfoService InfoService { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    private InfoViewModel Model { get; set; } = new();

    private string FormTitle { get; set; } = "Add Information";
    private string FormButtonText { get; set; } = "Add";
    private bool IsEdit { get; set; } = false;
    private bool IsInitialized { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        IsInitialized = true;

        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        if(!IsInitialized)
            return;

        var info = await InfoService.GetAsync();

        if (info.Id > 0)
        {
            Model = new InfoViewModel(info);

            FormTitle = "Edit Information";
            FormButtonText = "Edit";
            IsEdit = true;
        }
    }

    private async Task SubmitAsync()
    {
        if (IsEdit)
        {
            var response = await InfoService.UpdateAsync(Model.ToDto());

            await ShowToast(response);
        }
        else
        {
            var response = await InfoService.CreateAsync(Model.ToDto());

            await ShowToast(response);
        }

        await LoadDataAsync();
    }

    private async Task DeleteAsync()
    {
        var response = await InfoService.DeleteAsync(Model.Id);

        Model = new InfoViewModel();

        FormTitle = "Add Information";
        FormButtonText = "Add";
        IsEdit = false;

        await ShowToast(response);
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


    public class InfoViewModel
    {
        public InfoViewModel() { }

        public InfoViewModel(InfoDto infoDto)
        {
            Id = infoDto.Id;
            AboutMe = infoDto.AboutMe;
            GithubUrl = infoDto.GithubUrl;
            LinkedInUrl = infoDto.LinkedInUrl;
            Email = infoDto.Email;
        }

        public int Id { get; set; }
        [Required] public string AboutMe { get; set; } = string.Empty;
        [Required] public string GithubUrl { get; set; } = string.Empty;
        [Required] public string LinkedInUrl { get; set; } = string.Empty;
        [Required] public string Email { get; set; } = string.Empty;

        public InfoDto ToDto() => new(Id, AboutMe, GithubUrl, LinkedInUrl, Email);
    }
}