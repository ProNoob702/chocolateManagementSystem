using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.CreateChocolateStock;
using FluentValidation;

namespace hocolateManagementSystem.Application.Features.WholesalerChocolateSale.CreateChocolateStock;

public class CreateChocolateStockCommandValidator : AbstractValidator<CreateChocolateStockCommand>
{
    private readonly IChocolateBarsRepository _chocolateBarsRepository;
    private readonly IWholesalersRepository _wholesalersRepository;

    public CreateChocolateStockCommandValidator(IChocolateBarsRepository chocolateBarsRepository, IWholesalersRepository wholesalersRepository)
    {

        _chocolateBarsRepository = chocolateBarsRepository;
        _wholesalersRepository = wholesalersRepository;

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
        return await _wholesalersRepository.GetByIdAsync(wholesalerId, cancellationToken) != null;
    }

    public async Task<bool> ChocolateShouldExists(int chocolateBarId, CancellationToken cancellationToken)
    {
        return await _chocolateBarsRepository.GetByIdAsync(chocolateBarId, cancellationToken) != null;
    }
}
