using Lafda.Data;
using Lafda.Entities;
using Lafda.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lafda.Repositories;

public class MainEventRepository : IMainEventRepository
{
    private readonly ApplicationDbContext _context;

    public MainEventRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MainEvent?> GetByIdAsync(int id)
    {
        return await _context.MainEvent
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<MainEvent>> GetAllAsync()
    {
        return await _context.MainEvent
            .ToListAsync();
    }

    public async Task AddAsync(MainEvent entity)
    {
        await _context.MainEvent.AddAsync(entity);
    }

    public void Update(MainEvent entity)
    {
        _context.MainEvent.Update(entity);
    }

    public void Delete(MainEvent entity)
    {
        _context.MainEvent.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}