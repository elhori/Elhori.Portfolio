using Elhori.Portfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elhori.Portfolio.Infra.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Skill> Skills { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Info> Information { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}