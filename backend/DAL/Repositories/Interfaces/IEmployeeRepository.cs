using DAL.Models;

namespace DAL.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task AddAsync(Employee employee);
    Task DeleteAsync(Guid id);
    Task<List<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(Guid id);
    Task UpdateAsync(Employee employee);
}