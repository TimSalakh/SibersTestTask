using DAL.Configurations;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts;

/// <summary>
/// Master-context class of the database.
/// Contains colletions of all project's entities.
/// </summary>
public class CompanyDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Objective> Objectives { get; set; }

    public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
        : base(options) { }

    /// <summary>
    /// Connections string.
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;" +
            "Database=Company;" +
            "TrustServerCertificate=True;" +
            "Trusted_Connection=True;");
    }

    /// <summary>
    /// Entities configurations.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new ObjectiveConfiguration());
    }
}
