using System.Collections.ObjectModel;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.GetAllChocolateBarsByFactory
{
    public class GetChocolateBarsByFactoryResponse
    {
        public IReadOnlyDictionary<string, ReadOnlyCollection<ChocolateBarDTO>> ChocolateBarsByFactory { get; set;}
    }
}
