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
    public DbSet<Country> Countries { get; set; }

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
        modelBuilder.Entity<Country>().HasKey(x => x.Id);

        // Configurazione delle relazioni
        #region Expense

        // Precisione per Amount
        modelBuilder.Entity<Expense>()
            .Property(e => e.Amount)
            .HasColumnType("decimal(18,2)");

        // Relazione con Travel
        modelBuilder.Entity<Expense>()
            .HasOne(e => e.Travel)
            .WithMany()
            .HasForeignKey(e => e.TravelId)
            .OnDelete(DeleteBehavior.Cascade); // Se un viaggio viene cancellato, eliminare anche le sue spese

        // Relazioni con User (CreatedBy, UpdatedBy, PaidBy)
        modelBuilder.Entity<Expense>()
            .HasOne(e => e.CreatedByUser)
            .WithMany()
            .HasForeignKey(e => e.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Expense>()
            .HasOne(e => e.UpdatedByUser)
            .WithMany()
            .HasForeignKey(e => e.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Expense>()
            .HasOne(e => e.PaidByUser)
            .WithMany()
            .HasForeignKey(e => e.PaidBy)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region SharedFile

        // Relazione con Travel
        modelBuilder.Entity<SharedFile>()
            .HasOne(sf => sf.Travel)
            .WithMany()
            .HasForeignKey(sf => sf.TravelId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SharedFile>()
            .HasOne(sf => sf.CreatedByUser)
            .WithMany()
            .HasForeignKey(sf => sf.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SharedFile>()
            .HasOne(sf => sf.UpdatedByUser)
            .WithMany()
            .HasForeignKey(sf => sf.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region TravelParticipant

        modelBuilder.Entity<TravelParticipant>()
            .HasOne(tp => tp.Travel)
            .WithMany()
            .HasForeignKey(tp => tp.TravelId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TravelParticipant>()
            .HasOne(tp => tp.User)
            .WithMany()
            .HasForeignKey(tp => tp.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurazione della chiave composta per TravelParticipant (TravelId, UserId)
        modelBuilder.Entity<TravelParticipant>()
            .HasIndex(tp => new { tp.TravelId, tp.UserId })
            .IsUnique();

        #endregion

        #region UsefulLink

        modelBuilder.Entity<UsefulLink>()
            .HasOne(ul => ul.Travel)
            .WithMany()
            .HasForeignKey(ul => ul.TravelId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UsefulLink>()
            .HasOne(ul => ul.CreatedByUser)
            .WithMany()
            .HasForeignKey(ul => ul.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UsefulLink>()
            .HasOne(ul => ul.UpdatedByUser)
            .WithMany()
            .HasForeignKey(ul => ul.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}
