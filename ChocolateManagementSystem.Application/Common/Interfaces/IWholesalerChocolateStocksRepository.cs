using ChocolateManagementSystem.Domain.Entities;

namespace ChocolateManagementSystem.Application.Common.Interfaces
{
    public interface IWholesalerChocolateStocksRepository : IGenericRepository<WholesalerChocolateStock>
    {
        Task<WholesalerChocolateStock> FindWholesalerChocolateStock(int WholesalerId, int ChocolateBarId, CancellationToken cancellationToken);
        Task<bool> WholesalerHasEnoughStock(int WholesalerId, int ChocolateBarId, int requestedQuantity, CancellationToken cancellationToken);

    }
}
