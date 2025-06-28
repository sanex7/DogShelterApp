using DogShelterApp.Data;
using DogShelterApp.Models;
using DogShelterApp.Services;

class Program
{
    static void Main()
    {
        var context = new DogShelterContext();
        var service = new DogService(context);
        var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DogShelterDB;";
        var dapper = new DapperHelper(connectionString);


        while (true)
        {
            Console.WriteLine("""
                1. Додати собаку
                2. Переглянути всіх собак
                3. Переглянути неусиновлених
                4. Переглянути усиновлених
                5. Пошук собаки
                6. Оновити дані собаки
                7. Додати опікуна
                8. Всиновити собаку
                9. Масово додати 20 собак
                0. Вийти
            """);

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Ім'я: ");
                    var name = Console.ReadLine();
                    Console.Write("Вік: ");
                    var age = int.Parse(Console.ReadLine()!);
                    Console.Write("Порода: ");
                    var breed = Console.ReadLine();
                    service.AddDog(new Dog { Name = name!, Age = age, Breed = breed! });
                    break;

                case "2":
                    foreach (var dog in dapper.GetAllDogs())
                        Console.WriteLine($"{dog.Id} - {dog.Name}, {dog.Breed}, {dog.Age} років, усиновлено: {dog.IsAdopted}");
                    break;

                case "3":
                    foreach (var dog in dapper.GetAvailableDogs())
                        Console.WriteLine($"{dog.Id} - {dog.Name}");
                    break;

                case "4":
                    foreach (var dog in dapper.GetAdoptedDogs())
                        Console.WriteLine($"{dog.Id} - {dog.Name}");
                    break;

                case "5":
                    Console.WriteLine("1 - По Id, 2 - По імені, 3 - По породі");
                    var searchOpt = Console.ReadLine();
                    switch (searchOpt)
                    {
                        case "1":
                            Console.Write("Id: ");
                            var id = int.Parse(Console.ReadLine()!);
                            var dog = dapper.FindDogById(id);
                            Console.WriteLine(dog != null ? $"{dog.Name}, {dog.Breed}" : "Не знайдено");
                            break;
                        case "2":
                            Console.Write("Ім'я: ");
                            var searchName = Console.ReadLine();
                            foreach (var d in dapper.FindDogByName(searchName!))
                                Console.WriteLine($"{d.Id} - {d.Name}");
                            break;
                        case "3":
                            Console.Write("Порода: ");
                            var breedSearch = Console.ReadLine();
                            foreach (var d in dapper.FindDogByBreed(breedSearch!))
                                Console.WriteLine($"{d.Id} - {d.Name}");
                            break;
                    }
                    break;

                case "6":
                    Console.Write("Id собаки: ");
                    var updateId = int.Parse(Console.ReadLine()!);
                    service.UpdateDog(updateId, dog =>
                    {
                        Console.Write("Нове ім'я: ");
                        dog.Name = Console.ReadLine()!;
                        Console.Write("Новий вік: ");
                        dog.Age = int.Parse(Console.ReadLine()!);
                        Console.Write("Нова порода: ");
                        dog.Breed = Console.ReadLine()!;
                        Console.Write("Усиновлено (true/false): ");
                        dog.IsAdopted = bool.Parse(Console.ReadLine()!);
                    });
                    break;

                case "7":
                    Console.Write("Ім’я опікуна: ");
                    var adopterName = Console.ReadLine();
                    Console.Write("Номер телефону: ");
                    var phone = Console.ReadLine();

                    var adopter = new Adopter { Name = adopterName!, PhoneNumber = phone! };
                    context.Adopters.Add(adopter);
                    context.SaveChanges();
                    Console.WriteLine("Опікуна додано!");
                    break;

                case "8":
                    Console.Write("Id собаки для всиновлення: ");
                    var dogId = int.Parse(Console.ReadLine()!);
                    Console.Write("Id опікуна: ");
                    var adopterId = int.Parse(Console.ReadLine()!);

                    var dogToAdopt = context.Dogs.Find(dogId);
                    var adopterFound = context.Adopters.Find(adopterId);

                    if (dogToAdopt != null && adopterFound != null)
                    {
                        dogToAdopt.IsAdopted = true;
                        dogToAdopt.AdopterId = adopterFound.Id;
                        context.SaveChanges();
                        Console.WriteLine("Собаку всиновлено!");
                    }
                    else
                    {
                        Console.WriteLine("Не знайдено собаку або опікуна.");
                    }
                    break;

                case "9":
                    var dogs = new List<Dog>();

                    for (int i = 1; i <= 20; i++)
                    {
                        dogs.Add(new Dog
                        {
                            Name = $"Dog{i}",
                            Age = new Random().Next(1, 15),
                            Breed = $"Breed{i}",
                            IsAdopted = false
                        });
                    }

                    context.Dogs.AddRange(dogs);
                    context.SaveChanges();

                    Console.WriteLine("20 собак успішно додано!");
                    break;

                case "0":
                    return;
            }

            Console.WriteLine("Натисніть Enter для продовження...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
