
using ChocolateManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChocolateManagementSystem.Infrastructure.Context;

public interface IChocolateSystemContext
{
    DbSet<ChocolateBar> ChocolateBars { get; }
    DbSet<ChocolateFactory> ChocolateFactories { get; }
    DbSet<Wholesaler> Wholesalers { get; }
    DbSet<WholesalerChocolateStock> WholesalersChocolateBarsStocks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

