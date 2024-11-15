using Elhori.Portfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elhori.Portfolio.Infra.Persistence.Configurations;

public class InfoConfig : IEntityTypeConfiguration<Info>
{
    public void Configure(EntityTypeBuilder<Info> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.AboutMe)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.GithubUrl)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.LinkedInUrl)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(50);
    }
}