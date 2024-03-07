using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Name)
            .IsRequired();

        builder
            .Property(e => e.Surname)
            .IsRequired();

        builder
            .Property(e => e.Patronymic)
            .IsRequired();

        builder
            .HasMany(e => e.Projects)
            .WithMany(p => p.Employees);
    }
}
    
