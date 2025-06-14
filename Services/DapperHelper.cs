using Dapper;
using DogShelterApp.Models;
using Microsoft.Data.SqlClient;

namespace DogShelterApp.Services;

public class DapperHelper
{
    private readonly string _connectionString;

    public DapperHelper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<Dog> GetAllDogs()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Dog>("SELECT * FROM Dogs");
    }

    public IEnumerable<Dog> GetAvailableDogs() =>
        GetAllDogs().Where(d => !d.IsAdopted);

    public IEnumerable<Dog> GetAdoptedDogs() =>
        GetAllDogs().Where(d => d.IsAdopted);

    public Dog? FindDogById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QueryFirstOrDefault<Dog>("SELECT * FROM Dogs WHERE Id = @id", new { id });
    }

    public IEnumerable<Dog> FindDogByName(string name)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Dog>("SELECT * FROM Dogs WHERE Name = @name", new { name });
    }

    public IEnumerable<Dog> FindDogByBreed(string breed)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Dog>("SELECT * FROM Dogs WHERE Breed = @breed", new { breed });
    }
}
