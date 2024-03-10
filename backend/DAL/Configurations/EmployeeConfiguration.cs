using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .HasMany(e => e.Projects)
            .WithMany(p => p.Employees);

        builder
            .HasMany(e => e.Objectives)
            .WithOne(o => o.Executor);
    }
}
    
