using Lafda.Entities;

namespace Lafda.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    // Task<User?> GetByIdAsync(int id);

    Task AddAsync(User user);

    // void Delete(User user);

    Task<bool> SaveChangesAsync();
}