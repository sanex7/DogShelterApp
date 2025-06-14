namespace DogShelterApp.Models;

public class Adopter
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    
    public List<Dog> Dogs { get; set; } = new();
}
