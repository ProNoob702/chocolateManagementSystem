using ChocolateManagementSystem.Domain.Entities;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.GetAllChocolateBars
{
    public class GetAllChocolateBarsResponse
    {
        public IReadOnlyCollection<ChocolateBar> ChocolateBars { get; set; } = Array.Empty<ChocolateBar>();
    }
}
