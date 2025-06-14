using DogShelterApp.Models;


public class Dog
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Breed { get; set; } = null!;
    public bool IsAdopted { get; set; } = false;

    public int? AdopterId { get; set; }
    public Adopter? Adopter { get; set; }
}
