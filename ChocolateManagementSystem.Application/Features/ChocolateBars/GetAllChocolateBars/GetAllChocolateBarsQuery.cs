using MediatR;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.GetAllChocolateBars
{
    public class GetAllChocolateBarsQuery : IRequest<List<GetAllChocolateBarsResponse>>
    {
    }
}
