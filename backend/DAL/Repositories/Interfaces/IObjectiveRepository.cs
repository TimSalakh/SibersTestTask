using DAL.Models;

namespace DAL.Repositories.Interfaces;

public interface IObjectiveRepository
{
    public Task AddAsync(Objective objective);
    public Task<IEnumerable<Objective>> GetAllAsync();
    public Task<Objective?> GetByIdAsync(Guid id);
    public Task UpdateAsync(Objective objective);
    public Task DeleteAsync(Guid id);
}
