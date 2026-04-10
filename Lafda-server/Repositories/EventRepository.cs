using Lafda.Data;
using Lafda.Entities;
using Lafda.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lafda.Repositories;

public class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _context;

    public EventRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await _context.Events
            .Include(e => e.User)
            .Include(e => e.MainEvent)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Event>> GetAllAsync()
    {
        return await _context.Events
            .Include(e => e.User)
            .Include(e => e.MainEvent)
            .ToListAsync();
    }

    public async Task AddAsync(Event entity)
    {
        await _context.Events.AddAsync(entity);
    }

    public void Update(Event entity)
    {
        _context.Events.Update(entity);
    }

    public void Delete(Event entity)
    {
        _context.Events.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}