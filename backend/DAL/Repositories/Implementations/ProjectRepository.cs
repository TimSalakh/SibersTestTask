using DAL.Contexts;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Interfaces;

public class ProjectRepository : IProjectRepository
{
    private readonly CompanyDbContext _context;

    public ProjectRepository(CompanyDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _context.Projects
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var projectToDelete = await _context.Projects
            .FindAsync(id);
        if (projectToDelete == null)
            return;
        _context.Projects.Remove(projectToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Project project)
    {
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Employee>?> GetEmployeesAsync(Project project)
    {
        var targetProject = await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == project.Id);

        return targetProject.Employees;
    }

    public async Task AddProjectAsync(Employee employee, Project project)
    {
        var targetEmployee = await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == employee.Id);

        employee.Projects.Add(project);
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<Employee> GetLeaderAsync(Project project)
    {
        var targetProject = await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == project.Id);

        return targetProject.Leader;
    }
}
