using APBD_6.Models;
using Microsoft.EntityFrameworkCore;
namespace APBD_6.Data;

public static class Seeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().HasData(
            new Country
            {
                IdCountry = 1,
                Name = "Poland"
            },
            new Country
            {
                IdCountry = 2,
                Name = "Germany"
            }
        );

        modelBuilder.Entity<Client>().HasData(
            new Client
            {
                IdClient = 1,
                FirstName = "John",
                LastName = "Smith",
                Email = "john@smith.com",
                Telephone = "111222333",
                Pesel = "98020212345"
            },
            new Client 
            { 
                IdClient = 2,
                FirstName = "Jake",
                LastName = "Doe",
                Email = "jake@doe.com",
                Telephone = "666555444",
                Pesel = "20020212345"
            },
            new Client()
            {
                IdClient = 3,
                FirstName = "Remove",
                LastName = "Me",
                Email = "Remove@Me.com",
                Telephone = "1337133733",
                Pesel = "1337133733"
            }
        );

        modelBuilder.Entity<Trip>().HasData(
            new Trip
            {
                IdTrip = 1,
                Name = "ABC",
                Description = "Lorem ipsum...",
                DateFrom = new DateTime(2025,06,30),
                DateTo = new DateTime(2025,07,05),
                MaxPeople = 20
            },
            new Trip
            {
                IdTrip = 2,
                Name = "XYZ",
                Description = "Lorem ipsum...",
                DateFrom = new DateTime(2025,07,02),
                DateTo = new DateTime(2025,07,05),
            },
            new Trip
                {
                    IdTrip = 3,
                    Name = "Testowa Wycieczka",
                    Description = "Lorem ipsum...",
                    DateFrom = new DateTime(2025,07,05),
                    DateTo = new DateTime(2025,07,15),
                }
        );

        modelBuilder.Entity("Country_Trip").HasData(
            new
            {
                IdCountry = 1,
                IdTrip = 1
            },
            new
            {
                IdCountry = 2,
                IdTrip = 1
            }
        );

        modelBuilder.Entity<Client_Trip>().HasData(
            new Client_Trip
            {
                IdClient = 1,
                IdTrip = 1,
                PaymentDate = null,
                RegisteredAt = new DateTime(2025,06,29),
            },
            new Client_Trip
            {
                IdClient = 2,
                IdTrip = 1,
                PaymentDate = new DateTime(2025, 05, 30),
                RegisteredAt = new DateTime(2025, 06, 30)
            }
        );
    }
}