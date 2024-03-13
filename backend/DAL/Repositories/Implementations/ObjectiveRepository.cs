using DAL.Contexts;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

/// <summary>
/// Repository that implements objective CRUD-interface
/// </summary>
public class ObjectiveRepository : IObjectiveRepository
{
    /// <summary>
    /// Context of database to interact with 
    /// </summary>
    private readonly CompanyDbContext _context;

    public ObjectiveRepository(CompanyDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Async objective addition method.
    /// Setting executor and project from DTO before addition.
    /// </summary>
    /// <param name="objective">Objective to add</param>
    public async Task AddAsync(Objective objective)
    {
        var executor = await _context.Employees
            .FindAsync(objective.ExecutorId);
        objective.Executor = executor!;

        var project = await _context.Projects
            .FindAsync(objective.ProjectId);
        objective.Project = project!;

        await _context.Objectives.AddAsync(objective);
        await _context.SaveChangesAsync();  
    }

    /// <summary>
    /// Async deleting by id.
    /// </summary>
    /// <param name="id">Target objective's id</param>
    public async Task DeleteAsync(Guid id)
    {
        var objectiveToDelete = await GetByIdAsync(id);
        _context.Objectives.Remove(objectiveToDelete);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Async method that gets all objectives including their 
    /// executor and projects.
    /// </summary>
    public async Task<IEnumerable<Objective>> GetAllAsync()
    {
        var objectives = await Task.Run(() =>
        {
            return _context.Objectives
            .AsNoTracking()
            .Include(o => o.Executor)
            .Include(o => o.Project);
        });

        return objectives;
    }

    /// <summary>
    /// Async get by id including objective's executor and project.
    /// </summary>
    public async Task<Objective?> GetByIdAsync(Guid id)
    {
        return await _context.Objectives
            .Include(o => o.Executor)
            .Include(o => o.Project)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    /// <summary>
    /// Async updating.
    /// </summary>
    /// <param name="project">Objective to update</param>
    public async Task UpdateAsync(Objective objective)
    {
        _context.Objectives.Update(objective);
        await _context.SaveChangesAsync();
    }
}
