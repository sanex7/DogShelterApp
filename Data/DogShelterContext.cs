using DogShelterApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DogShelterApp.Data;

public class DogShelterContext : DbContext
{
    public DbSet<Dog> Dogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DogShelterDB;");
    }
}
