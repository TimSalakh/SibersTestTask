using DAL.Models;

namespace DAL.Repositories.Interfaces;

/// <summary>
/// This is an interface which contains standart set
/// of async CRUD operations for objective entity.
/// Nothing special.
/// </summary>
public interface IObjectiveRepository
{
    public Task AddAsync(Objective objective);
    public Task<IEnumerable<Objective>> GetAllAsync();
    public Task<Objective?> GetByIdAsync(Guid id);
    public Task UpdateAsync(Objective objective);
    public Task DeleteAsync(Guid id);
}
