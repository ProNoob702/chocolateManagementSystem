using ChocolateManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChocolateManagementSystem.Infrastructure.Context;

public static class SeedDataExtension
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        // Seed Factories
        modelBuilder.Entity<ChocolateFactory>().HasData(
            new ChocolateFactory
            {
                Id = 1,
                Name = "Neuhaus"
            },
            new ChocolateFactory
            {
                Id = 2,
                Name = "ChocoPlus"
            }
        );

        // Seed ChocolateBars
        modelBuilder.Entity<ChocolateBar>().HasData(
            new ChocolateBar
            {
                Id = 1,
                Name = "White Chocolate",
                Cacao = 12.5m,
                Price = 9.5m,
                FactoryId = 1,
            },
            new ChocolateBar
            {
                Id = 2,
                Name = "Beast Chocolate",
                Cacao = 20m,
                Price = 22.99m,
                FactoryId = 1,
            },
            new ChocolateBar
            {
                Id = 3,
                Name = "Mix Chocolate",
                Cacao = 5m,
                Price = 5.5m,
                FactoryId = 1,
            },
            new ChocolateBar
            {
                Id = 4,
                Name = "Strawberry Chocolate",
                Cacao = 10m,
                Price = 12m,
                FactoryId = 2,
            },
            new ChocolateBar
            {
                Id = 5,
                Name = "Dark Chocolate",
                Cacao = 11m,
                Price = 9m,
                FactoryId = 2,
            }
        );

        // Seed Wholesalers
        modelBuilder.Entity<Wholesaler>().HasData(
            new Wholesaler
            {
                Id = 1,
                Name = "Mamadou"
            },
            new Wholesaler
            {
                Id = 2,
                Name = "Keita"
            },
            new Wholesaler
            {
                Id = 3,
                Name = "Heung"
            },
            new Wholesaler
            {
                Id = 4,
                Name = "Pedro"
            }
        );
       

    }
}
