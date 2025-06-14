using DogShelterApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DogShelterApp.Data;

public class DogShelterContext : DbContext
{
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<Adopter> Adopters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DogShelterDB;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dog>()
            .HasOne(d => d.Adopter)
            .WithMany(a => a.Dogs)
            .HasForeignKey(d => d.AdopterId);
    }
}

