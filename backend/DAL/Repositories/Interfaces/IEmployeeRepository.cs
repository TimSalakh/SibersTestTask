using DAL.Models;

namespace DAL.Repositories.Interfaces;

public interface IEmployeeRepository
{
    // Переделать интерфейс, оставить тут ТОЛЬКО CRUD, всю 
    // остальную логику вынести в Service папку.
    // Также продумать построение запросов, чтобы не конфликтовали 
    // два GET с одинаковой сигнатурой.
    Task AddAsync(string name, string surname, string patronymic);
    Task AddProjectAsync(Employee employee, Project project);
    Task DeleteAsync(Employee employee);
    Task DeleteAsync(Guid id);
    Task<List<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(Guid id);
    Task<List<Project>?> GetProjectsAsync(Guid id);
    Task UpdateAsync(Employee employee);
}