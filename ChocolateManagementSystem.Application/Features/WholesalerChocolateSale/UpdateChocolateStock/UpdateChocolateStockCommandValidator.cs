using ChocolateManagementSystem.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.UpdateChocolateStock;

public class UpdateChocolateStockCommandValidator : AbstractValidator<UpdateChocolateStockCommand>
{
    private readonly IChocolateSystemContext _context;

    public UpdateChocolateStockCommandValidator(IChocolateSystemContext context)
    {
        _context = context;

        RuleFor(v => v.WholesalerId)
         .GreaterThan(0).WithMessage("WholesalerId is required.");

        RuleFor(v => v.ChocolateBarId)
         .GreaterThan(0).WithMessage("ChocolateId is required.");

        RuleFor(m => m)
          .MustAsync(WholeSalerShouldHasStock).WithMessage("Wholesaler must have stock on this chocolate");

        RuleFor(v => v.Stock)
          .NotNull().WithMessage("Stock is required.")
          .GreaterThan(0).WithMessage("Stock must be positive.");
    }

    public async Task<bool> WholeSalerShouldHasStock(UpdateChocolateStockCommand cmd, CancellationToken cancellationToken)
    {
        return await _context.WholesalersChocolateBarsStocks
            .FirstOrDefaultAsync(x => x.ChocolateBarId == cmd.ChocolateBarId && x.WholesalerId == cmd.WholesalerId, cancellationToken) != null;
    }
}
