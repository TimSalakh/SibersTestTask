using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Interfaces;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly CompanyDbContext _context;

    public EmployeeRepository(CompanyDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

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

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _context.Employees
            .Include(e => e.Projects)
            .Include(e => e.Objectives)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var employeeToDelete = await GetByIdAsync(id);
        if (employeeToDelete == null)
            return;
        _context.Employees.Remove(employeeToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Objective>> GetObjectives(Guid employeetId)
    {
        var employeeWithObjectives = await _context.Employees
             .AsNoTracking()
             .Include(p => p.Objectives)
             .FirstOrDefaultAsync(e => e.Id == employeetId);

        return employeeWithObjectives!.Objectives;
    }
}
