﻿using DAL.Configurations;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts;

public class CompanyDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Project> Projects { get; set; }

    public CompanyDbContext()
    { }

    public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
        :base(options) 
    { 
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
    }
}