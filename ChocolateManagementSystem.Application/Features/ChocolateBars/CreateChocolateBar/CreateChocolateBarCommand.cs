using MediatR;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.CreateChocolateBar
{
    public class CreateChocolateBarCommand : IRequest<int>
    {
        public string Name { get; set; }
        public decimal Cacao { get; set; }
        public decimal Price { get; set; }
        public int FactoryId { get; set; }
    }
}
