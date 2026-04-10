using Lafda.Entities;

namespace Lafda.Repositories.Interfaces;

public interface ITheoryRepository
{
    Task<Theory?> GetByIdAsync(int id);
    Task<List<Theory>> GetAllAsync();

    Task AddAsync(Theory entity);
    void Update(Theory entity);
    void Delete(Theory entity);

    Task<bool> SaveChangesAsync();
}