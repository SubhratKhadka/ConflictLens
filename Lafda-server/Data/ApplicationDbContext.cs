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
            o => o.UseVector()
        );
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresExtension("vector");

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

            // constraints adding
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_disorder_type", "disorder_type IN ('PoliticalViolence')");
                t.HasCheckConstraint(
                    "CK_event_type",
                    "event_type IN ('Battles', 'Explosions/Remote violence')"
                );
                t.HasCheckConstraint("CK_human_casualties", "human_casualties >= 0");
            });
        });

        modelBuilder.Entity<Theory>(entity =>
        {
            entity
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasOne(t => t.MainEvent)
                .WithMany()
                .HasForeignKey(t => t.MainEventId)
                .OnDelete(DeleteBehavior.Cascade); // Deleting the main event deletes the theory

            // Optional Relationship
            entity
                .HasOne(t => t.Event)
                .WithMany()
                .HasForeignKey(t => t.EventId)
                .OnDelete(DeleteBehavior.SetNull); // Deleting the secondary event just clears the link
        });
    }

    // entities register
    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<MainEvent> MainEvent { get; set; }
    public DbSet<Theory> Theories { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
