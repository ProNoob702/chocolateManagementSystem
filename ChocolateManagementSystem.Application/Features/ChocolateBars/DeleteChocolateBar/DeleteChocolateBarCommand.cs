using MediatR;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.DeleteChocolateBar
{
    internal class DeleteChocolateBarCommand : IRequest
    {
        public int ChocolateBarId { get; set; }
    }
}
