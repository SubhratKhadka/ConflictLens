using Lafda.Entities;

namespace Lafda.Repositories.Interfaces;

public interface IEventRepository
{
    Task<Event?> GetByIdAsync(int id);
    Task<List<Event>> GetAllAsync();

    Task AddAsync(Event entity);
    void Update(Event entity);
    void Delete(Event entity);

    Task<bool> SaveChangesAsync();
}