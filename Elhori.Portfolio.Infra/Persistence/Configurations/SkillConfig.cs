using Elhori.Portfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elhori.Portfolio.Infra.Persistence.Configurations;

public class SkillConfig : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Icon)
            .IsRequired()
            .HasMaxLength(50);
    }
}