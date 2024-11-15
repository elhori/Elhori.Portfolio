using System;
using Elhori.Portfolio.Application.Contracts.Repositories;
using Elhori.Portfolio.Infra.Persistence;
using Elhori.Portfolio.Infra.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Elhori.Portfolio.Infra;

public static class InfrastructureContainer
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContextFactory<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("default")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}