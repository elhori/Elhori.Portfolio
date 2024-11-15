using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Elhori.Portfolio.Client;

internal class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddScoped<ISkillService, SkillService>();
        builder.Services.AddScoped<IInfoService, InfoService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();

        await builder.Build().RunAsync();
    }
}