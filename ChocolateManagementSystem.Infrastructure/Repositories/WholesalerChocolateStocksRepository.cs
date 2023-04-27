using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Domain.Entities;
using ChocolateManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ChocolateManagementSystem.Infrastructure.Repositories;

public class WholesalerChocolateStocksRepository : GenericRepository<WholesalerChocolateStock>, IWholesalerChocolateStocksRepository
{
    public WholesalerChocolateStocksRepository(ChocolateSystemContext dbContext) : base(dbContext)
    {
    }

    public async Task<WholesalerChocolateStock?> FindWholesalerChocolateStock(int WholesalerId, int ChocolateBarId, CancellationToken cancellationToken)
    {
       return await _context.WholesalersChocolateBarsStocks.FirstOrDefaultAsync(x => x.WholesalerId == WholesalerId && x.ChocolateBarId == ChocolateBarId, cancellationToken);
    }

    public async Task<bool> WholesalerHasEnoughStock(int WholesalerId, int ChocolateBarId, int requestedQuantity, CancellationToken cancellationToken)
    {
        return await _context.WholesalersChocolateBarsStocks.FirstOrDefaultAsync(x => x.ChocolateBarId == ChocolateBarId && x.WholesalerId == WholesalerId && x.Stock > requestedQuantity, cancellationToken) != null;
    }
}
