using DAL.Contexts;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class ObjectiveRepository : IObjectiveRepository
{
    private readonly CompanyDbContext _context;

    public ObjectiveRepository(CompanyDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Objective objective)
    {
        var creator = await _context.Employees
            .FindAsync(objective.CreatorId);
        objective.Creator = creator!;

        var executor = await _context.Employees
            .FindAsync(objective.ExecutorId);
        objective.Executor = executor!;

        var project = await _context.Projects
            .FindAsync(objective.ProjectId);
        objective.Project = project!;

        await _context.Objectives.AddAsync(objective);
        await _context.SaveChangesAsync();  
    }

    public async Task DeleteAsync(Guid id)
    {
        var objectiveToDelete = await GetByIdAsync(id);
        if (objectiveToDelete == null) 
            return;
        _context.Objectives.Remove(objectiveToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Objective>> GetAllAsync()
    {
        var objectives = await Task.Run(() =>
        {
            return _context.Objectives
            .AsNoTracking()
            .Include(o => o.Creator)
            .Include(o => o.Executor)
            .Include(o => o.Project);
        });

        return objectives;
    }

    public async Task<Objective?> GetByIdAsync(Guid id)
    {
        return await _context.Objectives
            .Include(o => o.Creator)
            .Include(o => o.Executor)
            .Include(o => o.Project)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task UpdateAsync(Objective objective)
    {
        _context.Objectives.Update(objective);
        await _context.SaveChangesAsync();
    }
}
