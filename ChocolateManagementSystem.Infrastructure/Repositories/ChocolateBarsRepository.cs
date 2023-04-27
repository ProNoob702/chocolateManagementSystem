using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Domain.Entities;
using ChocolateManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ChocolateManagementSystem.Infrastructure.Repositories;

public class ChocolateBarsRepository : GenericRepository<ChocolateBar>, IChocolateBarsRepository
{
    public ChocolateBarsRepository(ChocolateSystemContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<ChocolateBar>> FetchAllWithFactoryFilled(CancellationToken cancellationToken)
    {
        return await _context.ChocolateBars.AsNoTracking()
               .Include(x => x.Factory)
               .OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<IEnumerable<ChocolateBar>> FetchByIds(HashSet<int> ids, CancellationToken cancellationToken)
    {
        return await _context.ChocolateBars.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<bool> NameDoesntExists(string name, CancellationToken cancellationToken)
    {
        return await _context.ChocolateBars.AllAsync(x => x.Name != name, cancellationToken);
    }
}