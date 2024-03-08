using DAL.Models;

namespace DAL.Repositories.Interfaces;

public interface IProjectRepository
{
    Task AddAsync(Project project);
    Task AddProjectAsync(Employee employee, Project project);
    Task DeleteAsync(Guid id);
    Task DeleteAsync(Project project);
    Task<List<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(Guid id);
    Task<List<Employee>?> GetEmployeesAsync(Project project);
    Task UpdateAsync(Project project);
    Task<Employee> GetLeaderAsync(Project project);
}
