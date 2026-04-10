using Lafda.Entities;

namespace Lafda.Repositories.Interfaces;

public interface IMainEventRepository
{
    Task<MainEvent?> GetByIdAsync(int id);
    Task<List<MainEvent>> GetAllAsync();

    Task AddAsync(MainEvent entity);
    void Update(MainEvent entity);
    void Delete(MainEvent entity);

    Task<bool> SaveChangesAsync();
}