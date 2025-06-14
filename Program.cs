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

                case "0":
                    return;
            }

            Console.WriteLine("Натисніть Enter для продовження...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
