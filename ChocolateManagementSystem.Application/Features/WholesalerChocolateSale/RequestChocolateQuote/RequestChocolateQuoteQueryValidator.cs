using ChocolateManagementSystem.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.RequestChocolateQuote;

public class RequestChocolateQuoteQueryValidator : AbstractValidator<RequestChocolateQuoteQuery>
{

    private readonly IChocolateSystemContext _context;

    public RequestChocolateQuoteQueryValidator(IChocolateSystemContext context)
    {
        _context = context;

        RuleFor(v => v.WholesalerId)
           .NotNull().WithMessage("WholesalerId is required.")
           .MustAsync(WholesalerIdShouldExists).WithMessage("The specified Wholesaler should exists.");

        RuleFor(v => v.OrderedItems)
            .NotNull().WithMessage("OrderedItems is required.")
            .Must(ShouldContainSomeItems).WithMessage("OrderedItems cannot be empty")
            .Must(NoDuplicatesInOrderedItems).WithMessage("The specified items should not contain duplicates.");

        RuleForEach(model => model.OrderedItems)
            .SetValidator(x => new OrderItemValidator(_context, x.WholesalerId));
        
    }

    public async Task<bool> WholesalerIdShouldExists(int wholesalerId, CancellationToken cancellationToken)
    {
        return await _context.Wholesalers.FirstOrDefaultAsync(x => x.Id == wholesalerId, cancellationToken) != null;
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

    private readonly IChocolateSystemContext _context;
    private readonly int _wholesalerId;

    public OrderItemValidator(IChocolateSystemContext context)
    {
        _context = context;
    }

    public OrderItemValidator(IChocolateSystemContext context, int wholesalerId)
    {
        _context = context;
        _wholesalerId = wholesalerId;

        RuleFor(v => v.ChocolateBarId)
            .NotNull().WithMessage("ChocolateBarId is required.")
            .MustAsync(WholesalerShouldHaveChocolate).WithMessage("The wholesaler doesn't sell this chocolate");

        RuleFor(m => m)
         .MustAsync(WholeSalerShouldHaveStock).WithMessage("quantity above wholesaler stock on this chocolate");

    }


    public async Task<bool> WholesalerShouldHaveChocolate(int chocolateBarId, CancellationToken cancellationToken)
    {
        return await _context.WholesalersChocolateBarsStocks.FirstOrDefaultAsync(x => x.ChocolateBarId == chocolateBarId && x.WholesalerId == _wholesalerId, cancellationToken) != null;
    }

    public async Task<bool> WholeSalerShouldHaveStock(OrderItem item, CancellationToken cancellationToken)
    {
        return await _context.WholesalersChocolateBarsStocks
            .FirstOrDefaultAsync(
            x => x.ChocolateBarId == item.ChocolateBarId && x.WholesalerId == _wholesalerId && x.Stock > item.Quantity
            , cancellationToken) != null;
    }
}