using DAL.Contexts;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Interfaces;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly CompanyDbContext _context;

    public EmployeeRepository(CompanyDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(string name, string surname, string patronymic)
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = name,
            Surname = surname,
            Patronymic = patronymic
        };
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var employeeToDelete = await _context.Employees
            .FindAsync(id);
        if (employeeToDelete == null)
            return;
        _context.Employees.Remove(employeeToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Employee employee)
    {
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Project>?> GetProjectsAsync(Guid id)
    {
        var targetEmployee = await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        return targetEmployee.Projects;
    }

    public async Task AddProjectAsync(Employee employee, Project project)
    {
        var targetProject = await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == project.Id);

        targetProject.Employees.Add(employee);
        _context.Projects.Update(targetProject);
        await _context.SaveChangesAsync();
    }
}
