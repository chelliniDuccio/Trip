using Microsoft.EntityFrameworkCore;
using Trip.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Definizione delle tabelle
    public DbSet<Travel> Travels { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<SharedFile> SharedFiles { get; set; }
    public DbSet<TravelParticipant> TravelParticipants { get; set; }
    public DbSet<UsefulLink> UsefulLinks { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Definizione della chiave primaria per ogni entità
        modelBuilder.Entity<Travel>().HasKey(x => x.Id);
        modelBuilder.Entity<Expense>().HasKey(x => x.Id);
        modelBuilder.Entity<SharedFile>().HasKey(x => x.Id);
        modelBuilder.Entity<TravelParticipant>().HasKey(x => x.Id);
        modelBuilder.Entity<UsefulLink>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().HasKey(x => x.Id);

        // Configurazione delle relazioni
        modelBuilder.Entity<Expense>()
            .HasOne(e => e.Travel)
            .WithMany(t => t.Expenses)
            .HasForeignKey(e => e.TravelId)
            .OnDelete(DeleteBehavior.Cascade); // Se elimini un Travel, elimini anche le sue spese

        modelBuilder.Entity<TravelParticipant>()
            .HasOne(tp => tp.Travel)
            .WithMany()
            .HasForeignKey(tp => tp.TravelId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TravelParticipant>()
            .HasOne(tp => tp.User)
            .WithMany()
            .HasForeignKey(tp => tp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UsefulLink>()
            .HasOne(ul => ul.Travel)
            .WithMany()
            .HasForeignKey(ul => ul.TravelId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SharedFile>()
            .HasOne(sf => sf.Travel)
            .WithMany()
            .HasForeignKey(sf => sf.TravelId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SharedFile>()
            .HasOne(sf => sf.User)
            .WithMany()
            .HasForeignKey(sf => sf.UploadedBy)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurazione della chiave composta per TravelParticipant (TravelId, UserId)
        modelBuilder.Entity<TravelParticipant>()
            .HasIndex(tp => new { tp.TravelId, tp.UserId })
            .IsUnique();
    }
}
