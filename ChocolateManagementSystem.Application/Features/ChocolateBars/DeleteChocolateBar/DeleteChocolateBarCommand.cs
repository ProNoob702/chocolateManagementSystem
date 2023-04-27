using ChocolateManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.DeleteChocolateBar;

public class DeleteChocolateBarCommand : IRequest
{
    public int ChocolateBarId { get; set; }
}

public class DeleteChocolateBarCommandHandler : IRequestHandler<DeleteChocolateBarCommand>
{
    private readonly IChocolateBarsRepository _chocolateBarsRepository;

    public DeleteChocolateBarCommandHandler(IChocolateBarsRepository chocolateBarsRepository)
    {
        _chocolateBarsRepository = chocolateBarsRepository;
    }

    public async Task Handle(DeleteChocolateBarCommand request, CancellationToken cancellationToken)
    {
        var entity = await _chocolateBarsRepository.GetByIdAsync(request.ChocolateBarId, cancellationToken);
        await _chocolateBarsRepository.DeleteAsync(entity!, cancellationToken);
    }
}