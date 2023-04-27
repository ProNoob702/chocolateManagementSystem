using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Domain.Entities;

namespace ChocolateManagementSystem.Application.Tests.MockData
{
    public class MockChocolateBarsRepository : IChocolateBarsRepository
    {

        private readonly List<ChocolateBar> _chocolateBars;

        public MockChocolateBarsRepository()
        {
            _chocolateBars = new List<ChocolateBar>()
            {
                new ChocolateBar
                {
                    Id = 1,
                    Name = "White Chocolate",
                    Cacao = 12.5m,
                    Price = 9.5m,
                    FactoryId = 1,
                },
                new ChocolateBar
                {
                    Id = 2,
                    Name = "Beast Chocolate",
                    Cacao = 20m,
                    Price = 22.99m,
                    FactoryId = 1,
                },
                new ChocolateBar
                {
                    Id = 3,
                    Name = "Mix Chocolate",
                    Cacao = 5m,
                    Price = 5.5m,
                    FactoryId = 2,
                }
            };
        }

        public async Task<IReadOnlyList<ChocolateBar>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() => _chocolateBars);
        }

        public async Task<IEnumerable<ChocolateBar>> FetchByIds(HashSet<int> ids, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _chocolateBars.Where(x => ids.Contains(x.Id)).ToList());
        }

        public async Task<ChocolateBar?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var existing = await Task.Run(() => _chocolateBars.FirstOrDefault(x => x.Id == id));
            return existing;
        }

        public async Task<bool> NameDoesntExists(string name, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _chocolateBars.All(x => x.Name != name));
        }

        public async Task<ChocolateBar> AddAsync(ChocolateBar newEntity, CancellationToken cancellationToken)
        {
            newEntity.Id = _chocolateBars.Count + 1;
            await Task.Run(() => _chocolateBars.Add(newEntity));
            return newEntity;
        }

        public async Task DeleteAsync(ChocolateBar entity, CancellationToken cancellationToken)
        {
            var i = _chocolateBars.FindIndex(x => x.Id == entity.Id);
            await Task.Run(() => _chocolateBars.RemoveAt(i));
        }

        public async Task UpdateAsync(ChocolateBar newOne, CancellationToken cancellationToken)
        {
            var i = await Task.Run(() => _chocolateBars.FindIndex(x => x.Id == newOne.Id));
            _chocolateBars[i] = newOne;
        }

        public async Task<IEnumerable<ChocolateBar>> FetchAllWithFactoryFilled(CancellationToken cancellationToken)
        {
            var chocolateFactoryRepo = new MockChocolateFactoryRepository();

            var result = new List<ChocolateBar>();

            foreach (var choco in _chocolateBars)
            {
                var chocoFatctoryObj = await chocolateFactoryRepo.GetByIdAsync(choco.FactoryId, CancellationToken.None);
                var filledChoco = new ChocolateBar
                {
                    Id = choco.Id,
                    Name = choco.Name,
                    Price = choco.Price,
                    Cacao = choco.Cacao,
                    FactoryId = choco.FactoryId,
                    Factory = chocoFatctoryObj!,
                };
                result.Add(filledChoco);
            }
            return result;
        }
    }
}
