using Lafda.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lafda.Data;

public class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection"), 
            o=> o.UseVector());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // convrt c# (in our case bit) to string in db
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(x => x.Status).HasConversion<string>();
            entity.Property(x => x.Role).HasConversion<string>();
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(x => x.Status).HasConversion<string>();
            entity.Property(x => x.DisorderType).HasConversion<string>();
            entity.Property(x => x.Embedding).HasColumnType("vector(1024)");
        });
    }

    // entities register
    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Theory> Theories { get; set; }
}

// TODO: python api for embedding of "BAAI/bge-m3"