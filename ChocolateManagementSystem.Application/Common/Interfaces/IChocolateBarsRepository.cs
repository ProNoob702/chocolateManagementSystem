using ChocolateManagementSystem.Domain.Entities;

namespace ChocolateManagementSystem.Application.Common.Interfaces;

public interface IChocolateBarsRepository : IGenericRepository<ChocolateBar>
{
    Task<bool> NameDoesntExists(string name, CancellationToken cancellationToken);
    Task<IEnumerable<ChocolateBar>> FetchAllWithFactoryFilled(CancellationToken cancellationToken);
    Task<IEnumerable<ChocolateBar>> FetchByIds(HashSet<int> ids , CancellationToken cancellationToken);
}
