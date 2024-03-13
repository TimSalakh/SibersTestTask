using DAL.Models;

namespace DAL.Repositories.Interfaces;

/// <summary>
/// This is an interface which contains standart set
/// of async CRUD operations for employee entity.
/// Contains functions for addition and deleting 
/// concrete employee to the concrete project.
/// </summary>
public interface IProjectRepository
{
    Task AddAsync(Project project);
    Task AddEmployee(Guid emploeeId, Guid projectId);
    Task DeleteEmployee(Guid emploeeId, Guid projectId);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(Guid id);
    Task UpdateAsync(Project project);
}
