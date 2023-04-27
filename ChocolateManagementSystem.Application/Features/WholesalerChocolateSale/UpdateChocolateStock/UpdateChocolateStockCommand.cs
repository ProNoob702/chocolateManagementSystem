using ChocolateManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.UpdateChocolateStock;

public class UpdateChocolateStockCommand : IRequest
{
    public int WholesalerId { get; set; }
    public int ChocolateBarId { get; set; }
    public int Stock { get; set; }
}

public class UpdateChocolateStockCommandHandler : IRequestHandler<UpdateChocolateStockCommand>
{
    private readonly IWholesalerChocolateStocksRepository _wholesalerChocolateStocksRepository;

    public UpdateChocolateStockCommandHandler(IWholesalerChocolateStocksRepository wholesalerChocolateStocksRepository)
    {
       _wholesalerChocolateStocksRepository = wholesalerChocolateStocksRepository;
    }

    public async Task Handle(UpdateChocolateStockCommand request, CancellationToken cancellationToken)
    {
        var entity = await _wholesalerChocolateStocksRepository.FindWholesalerChocolateStock(request.WholesalerId, request.ChocolateBarId, cancellationToken);

        entity!.Stock = request.Stock;

        await _wholesalerChocolateStocksRepository.UpdateAsync(entity, cancellationToken);
    }
}