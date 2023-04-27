using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Domain.Entities;
using ChocolateManagementSystem.Infrastructure.Context;

namespace ChocolateManagementSystem.Infrastructure.Repositories;

public class ChocolateFactoryRepository : GenericRepository<ChocolateFactory>, IChocolateFactoryRepository
{
    public ChocolateFactoryRepository(ChocolateSystemContext dbContext) : base(dbContext)
    {
    }
}
