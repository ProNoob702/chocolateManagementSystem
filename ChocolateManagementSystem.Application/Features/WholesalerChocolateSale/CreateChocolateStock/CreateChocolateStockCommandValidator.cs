using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.CreateChocolateStock;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace hocolateManagementSystem.Application.Features.WholesalerChocolateSale.CreateChocolateStock;

public class CreateChocolateStockCommandValidator : AbstractValidator<CreateChocolateStockCommand>
{
    private readonly IChocolateSystemContext _context;

    public CreateChocolateStockCommandValidator(IChocolateSystemContext context)
    {
        _context = context;

        RuleFor(v => v.WholesalerId)
         .NotNull().WithMessage("WholesalerId is required.")
         .MustAsync(WholesalerShouldExists).WithMessage("The specified Wholesaler should exists.");

        RuleFor(v => v.ChocolateBarId)
         .NotNull().WithMessage("ChocolateId is required.")
         .MustAsync(ChocolateShouldExists).WithMessage("The specified chocolate should exists.");

        RuleFor(v => v.Stock)
          .NotNull().WithMessage("Stock is required.")
          .GreaterThan(0).WithMessage("Stock must be positive.");
    }

    public async Task<bool> WholesalerShouldExists(int wholesalerId, CancellationToken cancellationToken)
    {
        return await _context.Wholesalers.FirstOrDefaultAsync(x => x.Id == wholesalerId, cancellationToken) != null;
    }

    public async Task<bool> ChocolateShouldExists(int chocolateBarId, CancellationToken cancellationToken)
    {
        return await _context.ChocolateBars.FirstOrDefaultAsync(x => x.Id == chocolateBarId, cancellationToken) != null;
    }
}
