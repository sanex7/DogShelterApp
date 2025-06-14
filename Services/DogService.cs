using DogShelterApp.Data;
using DogShelterApp.Models;

namespace DogShelterApp.Services;

public class DogService
{
    private readonly DogShelterContext _context;

    public DogService(DogShelterContext context)
    {
        _context = context;
    }

    public void AddDog(Dog dog)
    {
        _context.Dogs.Add(dog);
        _context.SaveChanges();
    }

    public void UpdateDog(int id, Action<Dog> update)
    {
        var dog = _context.Dogs.Find(id);
        if (dog != null)
        {
            update(dog);
            _context.SaveChanges();
        }
    }
}
