using ChocolateManagementSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.DeleteChocolateBar;

public class DeleteChocolateBarCommand : IRequest
{
    public int ChocolateBarId { get; set; }
}

public class DeleteChocolateBarCommandHandler : IRequestHandler<DeleteChocolateBarCommand>
{
    private readonly IChocolateSystemContext _context;

    public DeleteChocolateBarCommandHandler(IChocolateSystemContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteChocolateBarCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ChocolateBars.FirstAsync(x => x.Id == request.ChocolateBarId, cancellationToken);

        _context.ChocolateBars.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}