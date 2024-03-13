using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Interfaces;

/// <summary>
/// Repository that implements employee CRUD-interface.
/// </summary>
public class EmployeeRepository : IEmployeeRepository
{
    /// <summary>
    /// Context of database to interact with.
    /// </summary>
    private readonly CompanyDbContext _context;

    public EmployeeRepository(CompanyDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Async addition method.
    /// </summary>
    /// <param name="employee">Employee to add</param>
    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Async get all method employees including employee's projects and objectives.
    /// </summary>
    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        var employees = await Task.Run(() =>
        {
            return _context.Employees
            .AsNoTracking()
            .Include(e => e.Projects)
            .Include(e => e.Objectives);
        });

        return employees;
    }

    /// <summary>
    /// Async get by id method including employee's projects and objectives.
    /// </summary>
    /// <param name="id">Id of target employee</param>
    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _context.Employees
            .Include(e => e.Projects)
            .Include(e => e.Objectives)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    /// <summary>
    /// Async update of employee's entity method.
    /// </summary>
    /// <param name="employee">Target employee to update</param>
    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Async delete by id method.
    /// </summary>
    /// <param name="id">Target employee's id</param>
    public async Task DeleteAsync(Guid id)
    {
        var employeeToDelete = await GetByIdAsync(id);
        _context.Employees.Remove(employeeToDelete);
        await _context.SaveChangesAsync();
    }
}
