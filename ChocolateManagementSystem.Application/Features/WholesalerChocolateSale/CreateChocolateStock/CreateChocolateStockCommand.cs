using AutoMapper;
using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Domain.Entities;
using MediatR;

namespace ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.CreateChocolateStock;

public class CreateChocolateStockCommand : IRequest<int>
{
    public int WholesalerId { get; set; }
    public int ChocolateBarId { get; set; }
    public int Stock { get; set;}
}

public class CreateChocolateStockCommandHandler : IRequestHandler<CreateChocolateStockCommand, int>
{
    private readonly IWholesalerChocolateStocksRepository _wholesalerChocolateStocksRepository;
    private readonly IMapper _mapper;

    public CreateChocolateStockCommandHandler(IWholesalerChocolateStocksRepository wholesalerChocolateStocksRepository, IMapper mapper)
    {
        _wholesalerChocolateStocksRepository = wholesalerChocolateStocksRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateChocolateStockCommand request, CancellationToken cancellationToken)
    {
        var newStock = _mapper.Map<WholesalerChocolateStock>(request);
        newStock = await _wholesalerChocolateStocksRepository.AddAsync(newStock, cancellationToken);
        return newStock.Id;
    }
}
