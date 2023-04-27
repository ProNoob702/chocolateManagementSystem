using ChocolateManagementSystem.Application.Common.Interfaces;
using FluentValidation;

namespace ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.UpdateChocolateStock;

public class UpdateChocolateStockCommandValidator : AbstractValidator<UpdateChocolateStockCommand>
{
    private readonly IWholesalerChocolateStocksRepository _wholesalerChocolateStocksRepository;

    public UpdateChocolateStockCommandValidator(IWholesalerChocolateStocksRepository wholesalerChocolateStocksRepository)
    {
        _wholesalerChocolateStocksRepository = wholesalerChocolateStocksRepository;

        RuleFor(v => v.WholesalerId)
         .GreaterThan(0).WithMessage("WholesalerId is required.");

        RuleFor(v => v.ChocolateBarId)
         .GreaterThan(0).WithMessage("ChocolateId is required.");

        RuleFor(m => m)
          .MustAsync(WholeSalerShouldHaveStock).WithMessage("Wholesaler must have stock on this chocolate");

        RuleFor(v => v.Stock)
          .NotNull().WithMessage("Stock is required.")
          .GreaterThan(0).WithMessage("Stock must be positive.");
    }

    public async Task<bool> WholeSalerShouldHaveStock(UpdateChocolateStockCommand cmd, CancellationToken cancellationToken)
    {
        return await _wholesalerChocolateStocksRepository.FindWholesalerChocolateStock(cmd.WholesalerId, cmd.ChocolateBarId, cancellationToken) != null;
    }
}
