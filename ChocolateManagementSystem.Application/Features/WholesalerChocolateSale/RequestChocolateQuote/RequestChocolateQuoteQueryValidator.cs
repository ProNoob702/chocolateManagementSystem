using ChocolateManagementSystem.Application.Common.Interfaces;
using FluentValidation;

namespace ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.RequestChocolateQuote;

public class RequestChocolateQuoteQueryValidator : AbstractValidator<RequestChocolateQuoteQuery>
{
    private readonly IWholesalersRepository _wholesalersRepository;
    private readonly IWholesalerChocolateStocksRepository _wholesalerChocolateStocksRepository;


    public RequestChocolateQuoteQueryValidator(IWholesalersRepository wholesalersRepository, IWholesalerChocolateStocksRepository wholesalerChocolateStocksRepository)
    {

        _wholesalersRepository = wholesalersRepository;
        _wholesalerChocolateStocksRepository = wholesalerChocolateStocksRepository;

        RuleFor(v => v.WholesalerId)
           .NotNull().WithMessage("WholesalerId is required.")
           .MustAsync(WholesalerIdShouldExists).WithMessage("The specified Wholesaler should exists.");

        RuleFor(v => v.OrderedItems)
            .NotNull().WithMessage("OrderedItems is required.")
            .Must(ShouldContainSomeItems).WithMessage("OrderedItems cannot be empty")
            .Must(NoDuplicatesInOrderedItems).WithMessage("The specified items should not contain duplicates.");

        RuleForEach(model => model.OrderedItems)
            .SetValidator(x => new OrderItemValidator(_wholesalerChocolateStocksRepository, x.WholesalerId));
    }

    public async Task<bool> WholesalerIdShouldExists(int wholesalerId, CancellationToken cancellationToken)
    {
        return await _wholesalersRepository.GetByIdAsync(wholesalerId, cancellationToken) != null;
    }

    public bool ShouldContainSomeItems(IEnumerable<OrderItem> OrderedItems)
    {
        return OrderedItems.Any();
    }

    public bool NoDuplicatesInOrderedItems(IEnumerable<OrderItem> OrderedItems)
    {
        var trace = new HashSet<int>();
        bool noDuplicate = true; 
        foreach (var item in OrderedItems)
        {
            if (trace.Contains(item.ChocolateBarId))
            {
                noDuplicate = false;
                break;
            }
            trace.Add(item.ChocolateBarId);
        }
        return noDuplicate;
    }

}

public class OrderItemValidator : AbstractValidator<OrderItem>
{

    private readonly IWholesalerChocolateStocksRepository _wholesalerChocolateStocksRepository;
    private readonly int _wholesalerId;

    public OrderItemValidator(IWholesalerChocolateStocksRepository wholesalerChocolateStocksRepository)
    {
        _wholesalerChocolateStocksRepository = wholesalerChocolateStocksRepository;
    }

    public OrderItemValidator(IWholesalerChocolateStocksRepository wholesalerChocolateStocksRepository, int wholesalerId)
    {
        _wholesalerChocolateStocksRepository = wholesalerChocolateStocksRepository;
        _wholesalerId = wholesalerId;

        RuleFor(v => v.ChocolateBarId)
            .NotNull().WithMessage("ChocolateBarId is required.")
            .MustAsync(WholesalerShouldHaveChocolate).WithMessage("The wholesaler doesn't sell this chocolate");

        RuleFor(m => m)
         .MustAsync(WholeSalerShouldHaveStock).WithMessage("quantity above wholesaler stock on this chocolate");

    }

    public async Task<bool> WholesalerShouldHaveChocolate(int chocolateBarId, CancellationToken cancellationToken)
    {
        return await _wholesalerChocolateStocksRepository.FindWholesalerChocolateStock(_wholesalerId, chocolateBarId, cancellationToken) != null;
    }

    public async Task<bool> WholeSalerShouldHaveStock(OrderItem item, CancellationToken cancellationToken)
    {
        return await _wholesalerChocolateStocksRepository.WholesalerHasEnoughStock(_wholesalerId, item.ChocolateBarId, item.Quantity, cancellationToken);
    }
}