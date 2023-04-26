using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChocolateManagementSystem.Infrastructure.Context;

public class ChocolateSystemContext : DbContext, IChocolateSystemContext
{

    public ChocolateSystemContext(DbContextOptions<ChocolateSystemContext> options) : base(options) { }

    public DbSet<ChocolateBar> ChocolateBars { get; set; }
    public DbSet<ChocolateFactory> ChocolateFactories { get; set; }
    public DbSet<Wholesaler> Wholesalers { get; set; }
    public DbSet<WholesalerChocolateStock> WholesalersChocolateBarsStocks { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }

}
