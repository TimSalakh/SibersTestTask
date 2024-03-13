using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Interfaces;

/// <summary>
/// Repository that implements projects CRUD-interface.
/// </summary>
public class ProjectRepository : IProjectRepository
{
    /// <summary>
    /// Context of database to interact with.
    /// </summary>
    private readonly CompanyDbContext _context;

    public ProjectRepository(CompanyDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Async addition.
    /// Setting leader from DTO before addition.
    /// </summary>
    /// <param name="project">Project to add</param>
    public async Task AddAsync(Project project)
    {
        var leader = await _context.Employees
            .FindAsync(project.LeaderId);
        project.Leader = leader;

        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Async gets all method including project's leader, employees
    /// and objectives.
    /// </summary>
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

    /// <summary>
    /// Async get method by id including project's leader, employees
    /// and objectives.
    /// </summary>
    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _context.Projects
            .Include(p => p.Leader)
            .Include(p => p.Employees)
            .Include(p => p.Objectives)
            .FirstOrDefaultAsync(p => p.Id == id);
    }


    /// <summary>
    /// Async project updating method.
    /// </summary>
    /// <param name="project">Project to update</param>
    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Async deleting project method.
    /// </summary>
    /// <param name="id">Target project's id</param>
    public async Task DeleteAsync(Guid id)
    {
        var projectToDelete = await GetByIdAsync(id);
        _context.Projects.Remove(projectToDelete);
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Async mtthod to addition employee to the project.
    /// </summary>
    /// <param name="projectId">Target employee's id</param>
    /// <param name="emploeeId">Target project's id</param>
    /// <returns></returns>
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

    /// <summary>
    /// Async method to deleting employee from the project.
    /// </summary>
    /// <param name="projectId">Target employee's id</param>
    /// <param name="emploeeId">Target project's id</param>
    /// <returns></returns>
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
}
