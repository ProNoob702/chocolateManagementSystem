using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Domain.Entities;

namespace ChocolateManagementSystem.Application.Tests.MockData
{
    public class MockChocolateFactoryRepository : IChocolateFactoryRepository
    {
        private readonly List<ChocolateFactory> _chocolateFactories;

        public MockChocolateFactoryRepository() {

            _chocolateFactories = new List<ChocolateFactory>()
            {
                new ChocolateFactory()
                {
                    Id = 1,
                    Name = "Neuhaus"
                },
                new ChocolateFactory()
                {
                    Id = 2,
                    Name = "ChocoPlus"
                }
            };

        }

        public async Task<IReadOnlyList<ChocolateFactory>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() => _chocolateFactories);
        }

        public async Task<ChocolateFactory?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var existing = await Task.Run(() => _chocolateFactories.FirstOrDefault(x => x.Id == id));
            return existing;
        }

        public Task<ChocolateFactory> AddAsync(ChocolateFactory entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ChocolateFactory entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ChocolateFactory entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
