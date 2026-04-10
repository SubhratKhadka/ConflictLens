using Lafda.Data;
using Lafda.Entities;
using Lafda.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lafda.Repositories;

public class TheoryRepository : ITheoryRepository
{
    private readonly ApplicationDbContext _context;

    public TheoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Theory?> GetByIdAsync(int id)
    {
        return await _context.Theories
            .Include(t => t.User)
            .Include(t => t.MainEvent)
            .Include(t => t.Event)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Theory>> GetAllAsync()
    {
        return await _context.Theories
            .Include(t => t.User)
            .Include(t => t.MainEvent)
            .Include(t => t.Event)
            .ToListAsync();
    }

    public async Task AddAsync(Theory entity)
    {
        await _context.Theories.AddAsync(entity);
    }

    public void Update(Theory entity)
    {
        _context.Theories.Update(entity);
    }

    public void Delete(Theory entity)
    {
        _context.Theories.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}