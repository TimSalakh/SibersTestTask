using DAL.Models;

namespace DAL.Repositories.Interfaces;

/// <summary>
/// This is an interface which contains standart set
/// of async CRUD operations for employee entity.
/// Nothing special.
/// </summary>
public interface IEmployeeRepository
{
    Task AddAsync(Employee employee);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(Guid id);
    Task UpdateAsync(Employee employee);
}