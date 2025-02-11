using Microsoft.EntityFrameworkCore;
using Trip.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Aggiungi un DbSet per ogni entità
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configurazioni opzionali
        modelBuilder.Entity<User>().HasKey(p => p.Id);
    }
}
