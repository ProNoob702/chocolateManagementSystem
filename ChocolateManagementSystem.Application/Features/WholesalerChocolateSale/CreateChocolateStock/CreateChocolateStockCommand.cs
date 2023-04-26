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
    private readonly IChocolateSystemContext _context;
    private readonly IMapper _mapper;

    public CreateChocolateStockCommandHandler(IChocolateSystemContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateChocolateStockCommand request, CancellationToken cancellationToken)
    {
        var newStock = _mapper.Map<WholesalerChocolateStock>(request);
        _context.WholesalersChocolateBarsStocks.Add(newStock);
        await _context.SaveChangesAsync(cancellationToken);
        return newStock.Id;
    }
}
