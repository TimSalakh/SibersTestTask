using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Name)
            .IsRequired();

        builder
            .Property(p => p.Customer)
            .IsRequired();

        builder
            .Property(p => p.Executor)
            .IsRequired();

        builder
            .Property(p => p.StartDate)
            .IsRequired();

        builder
            .Property(p => p.EndDate)
            .IsRequired();

        builder
            .Property(p => p.Priority)
            .IsRequired();

        builder
            .HasMany(p => p.Employees)
            .WithMany(e => e.Projects);

        builder
            .HasOne(p => p.Leader)
            .WithMany()
            .HasForeignKey(p => p.LeaderId)
            .IsRequired();
    }
}
