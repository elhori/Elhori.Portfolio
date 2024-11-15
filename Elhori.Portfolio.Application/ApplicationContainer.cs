using Microsoft.Extensions.DependencyInjection;

namespace Elhori.Portfolio.Application;

public static class ApplicationContainer
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}