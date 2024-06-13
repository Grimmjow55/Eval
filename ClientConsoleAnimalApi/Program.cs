using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AnimalApiConsoleClient
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RaceId { get; set; }
    }

    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static string token = string.Empty;

        static async Task Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:5023/");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Subscribe");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await Login();
                        break;
                    case "2":
                        await Subscribe();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

                if (!string.IsNullOrEmpty(token))
                {
                    break;
                }
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. List all animals");
                Console.WriteLine("2. List all races");
                Console.WriteLine("3. Create an animal");
                Console.WriteLine("4. Create a race");
                Console.WriteLine("5. Update an animal by ID");
                Console.WriteLine("6. Update a race by ID");
                Console.WriteLine("7. Delete an animal by ID");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");

                var selection = Console.ReadLine();
                switch (selection)
                {
                    case "1":
                        await ListAllAnimals();
                        break;
                    case "2":
                        await ListAllRaces();
                        break;
                    case "3":
                        await CreateAnimal();
                        break;
                    case "4":
                        await CreateRace();
                        break;
                    case "5":
                        await UpdateAnimalById();
                        break;
                    case "6":
                        await UpdateRaceById();
                        break;
                    case "7":
                        await DeleteAnimalById();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
            }
        }

        private static async Task Login()
        {
            Console.Clear();
            Console.Write("Enter your email: ");
            var email = Console.ReadLine();

            Console.Write("Enter your password: ");
            var password = Console.ReadLine();

            var response = await client.PostAsJsonAsync("/login", new User { Email = email, Password = password });

            if (response.IsSuccessStatusCode)
            {
                token = await response.Content.ReadAsStringAsync();
                token = token.Trim('"'); // Clean up any surrounding quotes
                Console.WriteLine("Login successful.");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                Console.WriteLine("Login failed.");
            }
        }

        private static async Task Subscribe()
        {
            Console.Clear();
            Console.Write("Enter your email: ");
            var email = Console.ReadLine();

            Console.Write("Enter your password: ");
            var password = Console.ReadLine();

            var response = await client.PostAsJsonAsync("/user/create", new User { Email = email, Password = password });

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Subscription successful. You can now login.");
            }
            else
            {
                Console.WriteLine("Subscription failed.");
            }
        }

        private static async Task ListAllAnimals()
        {
            Console.Clear();
            var animals = await client.GetFromJsonAsync<List<Animal>>("/animals");
            Console.WriteLine("Animals:");
            foreach (var animal in animals)
            {
                Console.WriteLine($"ID: {animal.Id}, Name: {animal.Name}, Description: {animal.Description}, RaceId: {animal.RaceId}");
            }
        }

        private static async Task ListAllRaces()
        {
            Console.Clear();
            var races = await client.GetFromJsonAsync<List<Race>>("/races");
            Console.WriteLine("Races:");
            foreach (var race in races)
            {
                Console.WriteLine($"ID: {race.Id}, Name: {race.Name}, Description: {race.Description}");
            }
        }

        private static async Task CreateAnimal()
        {
            Console.Clear();
            Console.WriteLine("Available races:");
            await ListAllRaces(); // Display the list of available races

            Console.Write("Enter animal name: ");
            var name = Console.ReadLine();

            Console.Write("Enter animal description: ");
            var description = Console.ReadLine();

            Console.Write("Enter race ID for the animal: ");
            var raceId = int.Parse(Console.ReadLine());

            var response = await client.PostAsJsonAsync("/animals/create", new Animal { Name = name, Description = description, RaceId = raceId });

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Animal created successfully.");
            }
            else
            {
                Console.WriteLine("Failed to create animal.");
            }
        }

        private static async Task CreateRace()
        {
            Console.Clear();
            Console.Write("Enter race name: ");
            var name = Console.ReadLine();

            Console.Write("Enter race description: ");
            var description = Console.ReadLine();

            var response = await client.PostAsJsonAsync("/races/create", new Race { Name = name, Description = description });

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Race created successfully.");
            }
            else
            {
                Console.WriteLine("Failed to create race.");
            }
        }

        private static async Task UpdateAnimalById()
        {
            Console.Clear();
            Console.Write("Enter animal ID to update: ");
            var id = int.Parse(Console.ReadLine());

            Console.Write("Enter new animal name: ");
            var name = Console.ReadLine();

            Console.Write("Enter new animal description: ");
            var description = Console.ReadLine();

            Console.Write("Enter new race ID for the animal: ");
            var raceId = int.Parse(Console.ReadLine());

            var response = await client.PutAsJsonAsync($"/animals/{id}", new { Id = id, Name = name, Description = description, RaceId = raceId });

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Animal updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update animal.");
            }
        }

        private static async Task UpdateRaceById()
        {
            Console.Clear();
            Console.Write("Enter race ID to update: ");
            var id = int.Parse(Console.ReadLine());

            Console.Write("Enter new race name: ");
            var name = Console.ReadLine();

            Console.Write("Enter new race description: ");
            var description = Console.ReadLine();

            var response = await client.PutAsJsonAsync($"/races/update/{id}", new { Id = id, Name = name, Description = description });

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Race updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update race.");
            }
        }

        private static async Task DeleteAnimalById()
        {
            Console.Clear();
            Console.Write("Enter animal ID to delete: ");
            var id = int.Parse(Console.ReadLine());

            var response = await client.DeleteAsync($"/animals/delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Animal deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete animal.");
            }
        }
    }
}
