using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Domain.Entities;
using ChocolateManagementSystem.Infrastructure.Context;

namespace ChocolateManagementSystem.Infrastructure.Repositories;

public class WholesalersRepository : GenericRepository<Wholesaler>, IWholesalersRepository
{
    public WholesalersRepository(ChocolateSystemContext dbContext) : base(dbContext)
    {
    }
}
