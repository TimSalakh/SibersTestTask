using DAL.Models;

namespace DAL.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task AddAsync(Employee employee);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(Guid id);
    Task UpdateAsync(Employee employee);
    Task<IEnumerable<Objective>> GetObjectives(Guid employeetId);
}