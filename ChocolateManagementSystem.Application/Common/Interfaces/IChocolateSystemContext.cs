
using ChocolateManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChocolateManagementSystem.Persistence.Context
{
    public interface IChocolateSystemContext
    {
        DbSet<ChocolateBar> ChocolateBars { get; }
        DbSet<ChocolateFactory> ChocolateFactories { get; }
        DbSet<Wholesaler> Wholesalers { get; }
        DbSet<WholesalerChocolateStock> WholesalersChocolateBarsStocks { get; }
    }
}
