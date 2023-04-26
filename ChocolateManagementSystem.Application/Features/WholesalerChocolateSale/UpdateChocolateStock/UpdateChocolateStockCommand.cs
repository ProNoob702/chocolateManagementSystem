using ChocolateManagementSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.UpdateChocolateStock;

public class UpdateChocolateStockCommand : IRequest
{
    public int WholesalerId { get; set; }
    public int ChocolateBarId { get; set; }
    public int Stock { get; set; }
}

public class UpdateChocolateStockCommandHandler : IRequestHandler<UpdateChocolateStockCommand>
{
    private readonly IChocolateSystemContext _context;

    public UpdateChocolateStockCommandHandler(IChocolateSystemContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateChocolateStockCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.WholesalersChocolateBarsStocks.FirstAsync(x => x.WholesalerId == request.WholesalerId && x.ChocolateBarId == request.ChocolateBarId, cancellationToken);

        entity.Stock = request.Stock;

        await _context.SaveChangesAsync(cancellationToken);
    }
}