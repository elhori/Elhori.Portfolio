using Elhori.Portfolio.Application;
using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Application.Services;
using Elhori.Portfolio.Components;
using Elhori.Portfolio.Infra;
using Elhori.Portfolio.Validators;
using FluentValidation;

namespace Elhori.Portfolio;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddApplicationServices();

        builder.Services.AddInfraServices(builder.Configuration);

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddControllers();

        builder.Services.AddValidatorsFromAssemblyContaining<SkillDtoValidator>();

        builder.Services.AddScoped<ISkillService, SkillService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<IInfoService, InfoService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.MapControllers();

        app.Run();
    }
}