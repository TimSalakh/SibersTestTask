using DAL.Models;

namespace DAL.Repositories.Interfaces;

public interface IProjectRepository
{
    Task AddAsync(Project project);
    Task AddEmployee(Guid emploeeId, Guid projectId);
    Task DeleteEmployee(Guid emploeeId, Guid projectId);
    Task DeleteAsync(Guid id);
    Task<List<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(Guid id);
    Task<List<Employee>> GetEmployees(Guid projectId);
    Task UpdateAsync(Project project);
}
