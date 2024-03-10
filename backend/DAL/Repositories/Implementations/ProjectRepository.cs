using DAL.Contexts;
using DAL.Models;
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
        var leader = await _context.Employees
            .FindAsync(project.LeaderId);
        project.Leader = leader;

        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        var projects = await Task.Run(() =>
        {
            return _context.Projects
            .AsNoTracking()
            .Include(p => p.Leader)
            .Include(p => p.Employees)
            .Include(p => p.Objectives);
        });

        return projects;
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _context.Projects
            .Include(p => p.Leader)
            .Include(p => p.Employees)
            .Include(p => p.Objectives)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var projectToDelete = await GetByIdAsync(id);
        if (projectToDelete == null)
            return;
        _context.Projects.Remove(projectToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task AddEmployee(Guid projectId, Guid emploeeId)
    {
        var targetEmployee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == emploeeId);

        var targetProject = await GetByIdAsync(projectId);

        if (targetEmployee == null || targetProject == null)
            return;

        targetProject.Employees.Add(targetEmployee);
        await UpdateAsync(targetProject);
    }

    public async Task DeleteEmployee(Guid projectId, Guid emploeeId)
    {
        var targetEmployee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == emploeeId);

        var targetProject = await GetByIdAsync(projectId);

        if (targetEmployee == null || targetProject == null)
            return;

        targetProject.Employees.Remove(targetEmployee);
        await UpdateAsync(targetProject);
    }

    public async Task<IEnumerable<Employee>> GetEmployees(Guid projectId)
    {
        var projectsWithEmployees = await _context.Projects
            .AsNoTracking()
            .Include(p => p.Employees)
            .FirstOrDefaultAsync(p => p.Id == projectId);

        return projectsWithEmployees!.Employees;
    }

    public async Task<IEnumerable<Objective>> GetObjectives(Guid projectId)
    {
        var projectsWithObjectives = await _context.Projects
            .AsNoTracking()
            .Include(p => p.Objectives)
            .FirstOrDefaultAsync(p => p.Id == projectId);

        return projectsWithObjectives!.Objectives;
    }
}
